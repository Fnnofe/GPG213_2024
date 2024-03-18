using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public HealthBar healthBar;

    // Reference to the post-processing volume in your scene
    public Volume postProcessingVolume;
    Vignette vignette;

    // Parameters for vignette effect
    public float damageVignetteIntensity = 0.5f; 
    public float vignetteFadeDuration = 1.0f;    
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        // Initialize vignette effect
        if (postProcessingVolume.profile.TryGet(out Vignette tempVignette))
        {
            vignette = tempVignette;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            PlayerTakeDamage(2);
            Debug.Log("ouch");

            // Trigger vignette effect
            StartCoroutine(TriggerVignetteEffect());
        }
    }

    void PlayerTakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    IEnumerator TriggerVignetteEffect()
    {
        float initialIntensity = vignette.intensity.value;

        // Increase vignette intensity
        vignette.intensity.value = damageVignetteIntensity;

        // Wait for a brief period
        yield return new WaitForSeconds(vignetteFadeDuration);

        // Fade back to normal vignette intensity
        float elapsedTime = 0f;
        while (elapsedTime < vignetteFadeDuration)
        {
            vignette.intensity.value = Mathf.Lerp(damageVignetteIntensity, initialIntensity, elapsedTime / vignetteFadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure vignette intensity is back to normal
        vignette.intensity.value = initialIntensity;
    }
}