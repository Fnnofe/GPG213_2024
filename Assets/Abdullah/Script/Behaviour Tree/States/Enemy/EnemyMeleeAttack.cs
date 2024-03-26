using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using BehaviorTree;

    public class EnemyMeleeAttack : TreeNode
    {
        [SerializeField] float attackSpeed;


        public override NodeState Evaluate()
        {
        Debug.Log("I'm in EnemyMeleeAttack");




        return NodeState.Sucess;

        }
    }
