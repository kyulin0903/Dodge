using UnityEngine;
using UnityEngine.InputSystem.HID;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 8f;

    public float dashSpeed = 25f;
    public float dashDuration = 0.12f;
    public float dashCooldown = 2.5f;

    private bool isDashing;
    private float dashTimer;
    private float cooldownTimer;
    private Vector3 dashDir = Vector3.forward;

    public float hitForce = 20f;     // 수평으로 밀어내는 세기
    public float hitUpForce = 6f;    // 위로 띄우는 세기
    public float hitSpin = 15f;      // 회전 세기

    private bool isDead;
    public TrailRenderer trail;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (trail != null) trail.emitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        if (cooldownTimer > 0f) cooldownTimer -= Time.deltaTime;

        Vector3 input = new Vector3(xInput, 0f, zInput);
        if (input.magnitude > 0.1f) dashDir = input.normalized;

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && cooldownTimer <= 0f)
        {
            isDashing = true;
            dashTimer = dashDuration;
            cooldownTimer = dashCooldown;
            if (trail != null) trail.emitting = true;
        }

        if (isDashing)
        {
            rb.linearVelocity = dashDir * dashSpeed;
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                isDashing = false;
                if (trail != null) trail.emitting = false;
            }
                return;
        }

        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;

        Vector3 nVelocity = new Vector3(xSpeed, 0f, zSpeed);
        rb.linearVelocity = nVelocity;
    }
    public bool IsDashing() { return isDashing; }

    public float GetCooldownRatio() { return Mathf.Max(0f, cooldownTimer / dashCooldown); }

    public void Die(Vector3 hitDir)
    {
        isDead = true;
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        gameManager.EndGame();
        rb.constraints = RigidbodyConstraints.None;
        rb.AddForce(hitDir * hitForce + Vector3.up * hitUpForce, ForceMode.Impulse);
        rb.AddTorque(Vector3.Cross(Vector3.up, hitDir) * hitSpin, ForceMode.Impulse);
        Invoke(nameof(Hide), 2f);
    }
    void Hide()
    {
        gameObject.SetActive(false);
    }
}
