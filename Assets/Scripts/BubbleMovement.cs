using System.Linq;
using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    public Vector3 velocity = new Vector3(0, 1, 0);  // Initial velocity
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = velocity;

        IgnoreBubbleCollisions();
    }

    void Update()
    {
        // hack this bullshit cause fuck it
        Vector3 dir = rb.linearVelocity.normalized;
        rb.linearVelocity = dir * 10;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bubble") || collision.gameObject.CompareTag("Player"));
        {
            return;
        }

        if (collision.contacts.Length > 0)
        {
            Vector3 normal = collision.contacts[0].normal;
            velocity = Vector3.Reflect(velocity, normal);

            rb.linearVelocity = velocity.normalized * rb.linearVelocity.magnitude;
        }
    }

    void IgnoreBubbleCollisions()
    {
        var allBubbles = GameObject.FindGameObjectsWithTag("Bubble").Concat(GameObject.FindGameObjectsWithTag("Player"));
        foreach (var bubble in allBubbles)
        {
            if (bubble != gameObject) // Ignore self-collision
            {
                Physics.IgnoreCollision(bubble.GetComponent<Collider>(), GetComponent<Collider>());
            }
        }
    }
}
