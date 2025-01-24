using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    public Vector3 velocity = new Vector3(0, +1, 0);

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void Update()
    {
        rb.linearVelocity = velocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        velocity = -velocity;
    }
}
