using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using BehaviorTree;

public class EnemyCastAttack : TreeNode
{
    [SerializeField] float attackSpeed;


    public override NodeState Evaluate()
    {




        return NodeState.Sucess;

    }
}
