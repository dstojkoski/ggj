using UnityEngine;

public class BubbleHitDetection : MonoBehaviour
{
    private Rigidbody rigidbody;
    public int counter = 3;
    public GameObject bubblePrefab;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (counter > 0)
            {
                counter--;
                GameManager.Instance.StartBubbleSplit(transform.localScale, transform.position, rigidbody.linearVelocity, counter, bubblePrefab);
            }
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
