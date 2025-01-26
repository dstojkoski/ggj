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

        if (collision.gameObject.CompareTag("wall"))
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
    }

    System.Collections.IEnumerator FadeAndDestroy()
    {
        yield return new WaitForSeconds(1.5f);

        float fadeDuration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            if (bulletMaterial != null)
            {
                Color newColor = originalColor;
                newColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
                bulletMaterial.color = newColor;
            }

            yield return null;
        }

        Destroy(gameObject);
    }
}