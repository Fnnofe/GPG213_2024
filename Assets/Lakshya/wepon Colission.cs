using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weponColission : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log(other.name);
            other.GetComponent<EnemyHealth>().TakeDamage(10);

        }
    }





}
