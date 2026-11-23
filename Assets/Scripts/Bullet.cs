using UnityEngine;
using UnityEngine.Audio;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    private Rigidbody brb;

    void Start()
    {
        if (GameManager.Instance != null) speed = GameManager.Instance.CurrentBulletSpeed;
        brb = GetComponent<Rigidbody>();
        brb.linearVelocity = transform.forward * speed;

        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void Destroy(Object obj);
    void Destroy(Object obj, float t);*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController pc = other.GetComponent<PlayerController>();

            if (pc != null && !pc.IsDashing())
            {
                pc.Die(transform.forward);
            }
        }
    }
}
