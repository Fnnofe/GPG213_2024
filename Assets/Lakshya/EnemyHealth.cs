using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    int currentHealth;
    public int maxHealth;
    public Slider healthSlider;
    public Slider easyHealthSlider;

    float percentage;

    void Start()
    {
        percentage =0.02f * maxHealth;
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        easyHealthSlider.maxValue = maxHealth;
        UpdateUI();
    }
    private void FixedUpdate()
    {
        easyHealthSlider.value = Mathf.Lerp(easyHealthSlider.value, currentHealth, 0.05f);
        if (easyHealthSlider.value <= percentage) Die();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        this.GetComponent<Animator>().SetTrigger("Hit");
        UpdateUI();

    }
    void UpdateUI()
    {
        //Health down than lerp health down.
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        healthSlider.value = currentHealth;
    }
   public void ButtonDamage()
    {
        currentHealth -= 40;
        UpdateUI();
    }
   public void ButtonHeal()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}