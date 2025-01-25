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

    void OnCollisionEnter(Collision collision)
    {
        // hack this bullshit cause fuck it
        Vector3 dir = rb.linearVelocity.normalized;
        rb.linearVelocity = dir * 10;

        if (collision.gameObject.CompareTag("Bubble"))
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
        GameObject[] allBubbles = GameObject.FindGameObjectsWithTag("Bubble");
        foreach (var bubble in allBubbles)
        {
            if (bubble != gameObject) // Ignore self-collision
            {
                Physics.IgnoreCollision(bubble.GetComponent<Collider>(), GetComponent<Collider>());
            }
        }
    }
}
