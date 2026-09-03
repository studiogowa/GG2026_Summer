using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    [SerializeField, Range(0.0f, 10.0f)] public float movementSpeed = 5.0f;
    public float maxHealth;
    public float currentHealth;

    public HealthBar healthBar;

    void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public IEnumerator TempSpeedChange(float multiplier, float duration)
    {
        movementSpeed *= multiplier;
        Debug.Log("Movement speed is " + movementSpeed);
        yield return new WaitForSeconds(duration);
        movementSpeed /= multiplier;
        Debug.Log("Movement speed is " + movementSpeed);
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp((currentHealth + amount), 0.0f, maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            // death
        }
    }
}
