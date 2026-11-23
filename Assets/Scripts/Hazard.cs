using UnityEngine;

public class Hazard : MonoBehaviour
{
    public Transform fill;
    public float fillDuration = 2f;

    private Vector3 fillFullScale;
    private float elapsed;

    void Awake()
    {
        fillFullScale = fill.localScale;
        fill.localScale = Vector3.zero;
    }

    void Update()
    {
        if (GameManager.Instance.IsGameover())
        {
            Destroy(gameObject);
            return;
        }

        elapsed += Time.deltaTime;
        float t = Mathf.Clamp01(elapsed / fillDuration);
        fill.localScale = fillFullScale * t;

        if (t >= 1f) Explode();
    }

    void Explode()
    {
        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player != null)
        {
            Vector3 a = transform.position;
            Vector3 b = player.transform.position;
            a.y = 0f;
            b.y = 0f;

            float radius = fillFullScale.x * 0.5f * transform.localScale.x;
            if (Vector3.Distance(a, b) <= radius) player.Die(transform.forward);
        }
        Destroy(gameObject);
    }
}