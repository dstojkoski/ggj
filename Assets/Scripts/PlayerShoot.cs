using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    public Camera playerCamera;
    public AudioSource shootAudioSource; // Add this for the audio source
    public AudioClip shootSound; // Add this for the sound clip
    public float pitchVariation = 0.2f; // Controls the range of pitch variation

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Spawn the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Vector3 shootDirection = GetShootDirection();
        bullet.transform.forward = shootDirection;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = shootDirection * bulletSpeed;

        // Play the shooting sound
        PlayShootSound();
    }

    Vector3 GetShootDirection()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return (hit.point - bulletSpawnPoint.position).normalized;
        }

        return playerCamera.transform.forward;
    }

    void PlayShootSound()
    {
        if (shootAudioSource != null && shootSound != null)
        {
            shootAudioSource.pitch = 1f + Random.Range(-pitchVariation, pitchVariation); // Add pitch variation
            shootAudioSource.PlayOneShot(shootSound); // Play the sound
        }
    }
}
