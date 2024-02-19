using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weponColission : MonoBehaviour
{
    public meleplayer mp;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log(other.name);
            other.GetComponent<Animator>().SetTrigger("Hit");

        }
    }


}
