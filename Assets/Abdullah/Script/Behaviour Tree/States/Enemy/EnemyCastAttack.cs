using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using BehaviorTree;

public class EnemyCastAttack : TreeNode
{
    [SerializeField] float attackSpeed;


    public override NodeState Evaluate()
    {


        Debug.Log("I'm in EnemyCastAttack");

        return NodeState.Failure;

    }
}
