using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthManager : MonoBehaviour
{
    public Slider healthBar;
    [Header("Player Health")]
    public FloatValue currentHealth;
    public FloatValue maxHealth;


    void DamageHealth(float number )
    {
        currentHealth.value -= number;
        if (currentHealth.value > 0)
        {
            //Trigger Effect 
            //Trigger Effect 
            //Trigger Effect 

        }
        else
        {
            currentHealth.value = 0;
            //Dead
            //Dead
            //Dead

        }
    }
    void RecoverHealth(float number)
    {
        //update health.
        // damage + healing
        currentHealth.value += number;
        if (currentHealth.value > maxHealth.value)
        {
            currentHealth.value = maxHealth.value;
        }


        //Trigger Effect 
        //Trigger Effect 
        //Trigger Effect 
    }
    

}
