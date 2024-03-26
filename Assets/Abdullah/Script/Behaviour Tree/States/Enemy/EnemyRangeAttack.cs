using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using BehaviorTree;

public class EnemyRangeAttack : TreeNode
{
    [SerializeField] float attackSpeed;


    public override NodeState Evaluate()
    {
        Debug.Log("I'm in EnemyRangeAttack");




        return NodeState.Sucess;

    }
}
