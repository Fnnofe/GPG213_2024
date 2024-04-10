using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public FloatValue currentHealth;
    public FloatValue maxHealth;

    public Slider healthSlider;
    public Slider easInHealthSlider;
    float initialIntensity;

    float percentage;

    // Reference to the post-processing volume in your scene
    public Volume postProcessingVolume;
    Vignette vignette;

    // Parameters for vignette effect
    public float damageVignetteIntensity = 0.5f; 
    public float vignetteFadeDuration = 1.0f;    
    void Start()
    {

        percentage = 0.02f * maxHealth.value;
        currentHealth.value = maxHealth.value;

        easInHealthSlider.maxValue = maxHealth.value;
        healthSlider.maxValue = maxHealth.value;
        UpdateUI();

        // Initialize vignette effect
        if (postProcessingVolume.profile.TryGet(out Vignette tempVignette))
        {
            vignette = tempVignette;
        }
        initialIntensity = vignette.intensity.value;

    }
    private void Update()
    {
        easInHealthSlider.value = Mathf.Lerp(easInHealthSlider.value, currentHealth.value, 0.05f);
        if (easInHealthSlider.value <= percentage)
        {
            // player died
            // player died
            // player died
        }

    }

    public void TakeDamage(int damage)
    {
        // Trigger vignette effect
        StartCoroutine(TriggerVignetteEffect());

        currentHealth.value -= damage;
        GetComponent<Animator>().SetTrigger("Hit");
        UpdateUI();

    }
    void UpdateUI()
    {
        //Health down than lerp health down.
        if (currentHealth.value < 0)
        {
            currentHealth.value = 0;
        }
        healthSlider.value = currentHealth.value;
    }


    IEnumerator TriggerVignetteEffect()
    {
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