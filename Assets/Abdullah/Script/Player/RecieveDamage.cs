using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecieveDamage : MonoBehaviour
{
    // Start is called before the first frame update
    public int meleeDamage = 12;


    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Enemy")
        {
            if (other.gameObject.tag == "PlayerAttack")
            {
                GetComponent<EnemyHealth>().TakeDamage(meleeDamage);
                Debug.Log("Enemy took damage");

            }
        }
        else if(gameObject.tag == "Player")
        {
            if (other.gameObject.tag == "Weapon") 
            {
            GetComponent<PlayerHealth>().TakeDamage(meleeDamage);
            Debug.Log("Player took damage");
            }
        }

    }
}
