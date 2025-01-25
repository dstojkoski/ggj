using UnityEngine;

public class BubbleSplitter : MonoBehaviour
{
    private Vector3 position;
    private Vector3 velocity;
    private int counter;
    private GameObject bubblePrefab;
    public float smallerScaleFactor = 0.5f;
    public float velocityOffset = 2f;
    public float positionOffset = 1f;

    public void Initialize(Vector3 position, Vector3 velocity, int counter, GameObject bubblePrefab)
    {
        this.position = position;
        this.velocity = velocity;
        this.counter = counter;
        this.bubblePrefab = bubblePrefab;

        CreateSmallerBubble(Vector3.left, -positionOffset);
        CreateSmallerBubble(Vector3.right, positionOffset);

        Destroy(gameObject);
    }

    private void CreateSmallerBubble(Vector3 direction, float positionOffset)
    {
        Vector3 newPosition = position + direction * positionOffset;

        GameObject smallerBubble = Instantiate(bubblePrefab, newPosition, Quaternion.identity);
        smallerBubble.transform.localScale = transform.localScale * smallerScaleFactor;

        BubbleHitDetection smallerBubbleScript = smallerBubble.GetComponent<BubbleHitDetection>();
        if (smallerBubbleScript != null)
        {
            smallerBubbleScript.counter = counter;
        }

        Rigidbody smallerBubbleRb = smallerBubble.GetComponent<Rigidbody>();
        if (smallerBubbleRb != null)
        {
            smallerBubbleRb.linearVelocity = velocity + direction * velocityOffset;
        }
    }
}
