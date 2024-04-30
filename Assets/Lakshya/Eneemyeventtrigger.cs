using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Eneemyeventtrigger : MonoBehaviour
{
   public EnemyTree[] enemies;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Entity entity;
        if (other.tag == "Player")
        {
            foreach (EnemyTree enemy in enemies)
            {
                enemy.enabled = true;
                entity = enemy.gameObject.GetComponent<Entity>();
                entity.enabled = true;
            }
        }
    }
}
