using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ability_Schizo : MonoBehaviour
{
    public GameObject OriginalObjects;
    public GameObject propsShift;
    Animator animator;
    public float MaxDuration;
    public Slider slider;
    float timer = 0.0f;



    void Start()
    {
        animator=GetComponent<Animator>();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Switch");
        }
        AbilityTimer();
    }

    private void AbilityTimer()
    {
        //actavate the ability
        if (timer <= MaxDuration && propsShift.activeSelf == true)
        {
            timer += 1 * Time.deltaTime;
            if (timer > MaxDuration)
            {
                timer = MaxDuration;
            }
            slider.value = timer/ MaxDuration;
        }
        //deactavate ability
        if (timer >0 && propsShift.activeSelf == false)
        {
            timer -=  1 * Time.deltaTime;
            if (timer < 0)
            {
                timer = 0;
            }
            float newValue = 
            slider.value = timer / MaxDuration;

        }
        animator.SetFloat("Timer", timer / MaxDuration);

    }

    public void SwitchObject()
    {
        if (propsShift.activeSelf == true)
        {
            propsShift.SetActive(false);
            OriginalObjects.SetActive(true);
        }

        else
        {
            propsShift.SetActive(true);
            OriginalObjects.SetActive(false);
        }

    }


}
