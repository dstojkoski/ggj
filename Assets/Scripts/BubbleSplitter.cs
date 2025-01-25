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

    // Initialize with position, velocity, counter, and bubble prefab
    public void Initialize(Vector3 scale, Vector3 position, Vector3 velocity, int counter, GameObject bubblePrefab)
    {
        this.position = position;
        this.velocity = velocity;
        this.counter = counter;
        this.bubblePrefab = bubblePrefab;

        // Create two smaller bubbles with random directions
        CreateSmallerBubble(scale, Vector3.zero, positionOffset);
        CreateSmallerBubble(scale, Vector3.zero, -positionOffset);

        Destroy(gameObject);
    }

    // Method to create smaller bubbles in a random direction
    private void CreateSmallerBubble(Vector3 scale, Vector3 direction, float positionOffset)
    {
        // Randomly choose a direction for the new bubble to move in
        Vector3 randomDirection = GetRandomDirection();
        Vector3 newPosition = position + randomDirection * positionOffset;

        GameObject smallerBubble = Instantiate(bubblePrefab, newPosition, Quaternion.identity);
        smallerBubble.transform.localScale = scale * smallerScaleFactor;

        // Update the counter on the smaller bubble
        BubbleHitDetection smallerBubbleScript = smallerBubble.GetComponent<BubbleHitDetection>();
        if (smallerBubbleScript != null)
        {
            smallerBubbleScript.counter = counter;
        }

        // Adjust the velocity of the smaller bubble
        Rigidbody smallerBubbleRb = smallerBubble.GetComponent<Rigidbody>();
        if (smallerBubbleRb != null)
        {
            smallerBubbleRb.linearVelocity = velocity + randomDirection * velocityOffset;
        }
    }

    // Method to return a random direction for the bubble to move
    private Vector3 GetRandomDirection()
    {
        // Generate a random direction using X, Y, and Z values
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);

        // Normalize the vector to ensure the direction is consistent
        return new Vector3(x, y, z).normalized;
    }
}
