using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public FloatValue health, sanity, maxHealth;
    // Update is called once per frame
    void Update()
    {
        if (health.value == 0) 
        { 
            health.value = maxHealth.value;
            sanity.value = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


}
