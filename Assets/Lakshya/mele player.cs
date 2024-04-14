using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleplayer : MonoBehaviour
{
    public float attackCooldown = 1f;

    bool canAttack = true;


    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canAttack)
            {
                MeeleAttack();
            }
        }
    }


    public void MeeleAttack()
    {
        canAttack = false;

        GetComponent<Animator>().SetTrigger("Attack");
        StartCoroutine(ResetCooldown());
    }


    IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
