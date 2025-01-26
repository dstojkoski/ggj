using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private Material bulletMaterial;
    private Color originalColor;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            bulletMaterial = renderer.material;
            originalColor = bulletMaterial.color;
        }
    }

    void OnCollisionEnter(Collision collision)
    {

            // Disable all movement and physics
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
            }

            // Snap to collision point and ensure correct orientation
            ContactPoint contact = collision.contacts[0];
            transform.position = contact.point + contact.normal * 0.01f; // Slight offset to avoid clipping
            transform.rotation = Quaternion.LookRotation(-contact.normal);

            // Optional: Detach the bullet from further physics influences
            transform.parent = null;

            StartCoroutine(FadeAndDestroy());
    }

    System.Collections.IEnumerator FadeAndDestroy()
    {
        yield return new WaitForSeconds(0.01f);

        Destroy(gameObject);
    }
}