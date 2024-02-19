using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleplayer : MonoBehaviour
{
    public GameObject meele;
    public bool canAttack = true;
    public float attackCooldown = 1f;
    public bool isAttacking = false;

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
        isAttacking = true;
        Animator anim = meele.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        StartCoroutine(ResetCooldown());
    }


    IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        isAttacking = false;
    }
}
