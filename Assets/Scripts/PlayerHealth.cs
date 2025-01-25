using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public float invincibilityTime = 3.0f;
    private int currentHealth;
    private bool isInvincible = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isInvincible)
        {
            // TODO: INDICATOR
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.CompareTag("Bubble") && !isInvincible)
        {
            TakeDamage(1);  // take 1 damage when hit by ball
            StartCoroutine(ActivateInvincibility(invincibilityTime)); // Activate invincibility for 3 seconds
        }
    }

    void TakeDamage(int damage)
    {
        Debug.Log("Damage");
        currentHealth -= damage;
        Debug.Log("Player took damage! Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator ActivateInvincibility(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }

    void Die()
    {
        Debug.Log("Player died!");
    }
}
