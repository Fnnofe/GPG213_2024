using UnityEngine;

public class CleansePlayer : MonoBehaviour
{
    Ability_Schizo playerToCleanse;
    PlayerHealth playerHealth;
    bool playerIsNear= false;
    public int cleanSpeed=2;
    public int recoverSpeed =8;
    // Start is called before the first frame update
    void Start()
    {
        playerToCleanse = FindAnyObjectByType<Ability_Schizo>();
        playerHealth = FindAnyObjectByType<PlayerHealth>();
    }
    private void Update()
    {
        if (playerIsNear==true)
        {
            CleansPLayer();
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player") 
        {
            playerIsNear = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsNear = false;
        }

    }

    public void CleansPLayer()
    {
        float currentHealth = playerHealth.currentHealth.value;
        float maxHealth = playerHealth.maxHealth.value;
        float currentSanity = playerToCleanse.sanityValue.value;

        if (currentSanity > 0) {
            playerToCleanse.slider.value = playerToCleanse.sanityValue.value / playerToCleanse.MaxDuration;
            playerToCleanse.sanityValue.value -= 1 * Time.deltaTime * cleanSpeed;

            Debug.Log("CLEANSING");
            
        }
        if (currentHealth <= maxHealth) {
            playerHealth.currentHealth.value = currentHealth + 1 * Time.deltaTime * recoverSpeed;
            playerHealth.healthSlider.value = playerHealth.currentHealth.value;

        }
    }
}
