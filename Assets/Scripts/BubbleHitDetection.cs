using UnityEngine;

public class BubbleHitDetection : MonoBehaviour
{
    private Rigidbody rigidbody;
    public int counter = 3;
    public GameObject bubblePrefab;

    public AudioSource hitAudioSource; 

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        //hitAudioSource.PlayOneShot(hitSound); // Play the sound
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Play the hit sound
            PlayHitSound();

            if (counter > 0)
            {
                counter--;
                GameManager.Instance.StartBubbleSplit(transform.localScale, transform.position, rigidbody.linearVelocity, counter, bubblePrefab);
            }

            Destroy(collision.gameObject);
            Destroy(gameObject, 0.01f);
        }
    }

    void PlayHitSound()
    {
        // Create a new GameObject for the audio source
        GameObject audioObject = new GameObject("HitAudioObject");

        // Add an AudioSource component to the new GameObject
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        // Assign the same audio clip from the original AudioSource
        audioSource.clip = hitAudioSource.clip;

        // Optionally, set the volume if needed
        audioSource.volume = hitAudioSource.volume;

        // Play the sound
        audioSource.Play();

        // Destroy the audio object after the sound has finished playing
        Destroy(audioObject, audioSource.clip.length);
    }
}
