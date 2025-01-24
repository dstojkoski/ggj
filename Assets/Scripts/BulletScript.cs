using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.linearVelocity = transform.forward * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        Vector3 collisionPoint = collision.contacts[0].point;
        Debug.Log("Collision at: " + collisionPoint);

        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
