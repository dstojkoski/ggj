using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  // For TextMeshPro
using UnityEngine.UI;  // For UI elements

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public float invincibilityTime = 3.0f;
    public TextMeshProUGUI healthText;  // Reference to the Text component
    public GameObject[] hearts;  // Array to hold heart UI images
    public GameObject redScreenPanel;  // Reference to the full-screen red overlay panel

    private int currentHealth;
    private bool isInvincible = false;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();  // Update the UI with the initial health
    }

    void Update()
    {
        if (isInvincible)
        {
            // TODO: Add visual indicators for invincibility
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bubble") && !isInvincible)
        {
            TakeDamage(1);  // Take 1 damage when hit by Bubble
            StartCoroutine(ActivateInvincibility(invincibilityTime)); // 3 seconds of invincibility

            // Activate the red screen panel
            StartCoroutine(ActivateRedScreenPanel(0.2f)); // Adjust the duration as needed
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();  // Update the UI when health changes
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

    IEnumerator ActivateRedScreenPanel(float duration)
    {
        redScreenPanel.SetActive(true);  // Activate the red panel
        yield return new WaitForSeconds(duration);
        redScreenPanel.SetActive(false);  // Deactivate the red panel
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth;  // Update the text on the UI
        }

        // Hide hearts based on current health
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].SetActive(true);  // Show heart if health is above this index
            }
            else
            {
                hearts[i].SetActive(false);  // Hide heart if health is below this index
            }
        }

        if (isInvincible)
        {
            healthText.text = "INVINCIBLE!! Health: " + currentHealth;  // Show invincibility message
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        SceneManager.LoadScene("GameOver");
        // Additional death handling logic
    }
}
