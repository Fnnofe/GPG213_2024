using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using BehaviorTree;

    public class EnemyMeleeAttack : TreeNode
    {
        [SerializeField] float attackSpeed;
        Transform _transform;
        Animator _animator;
    public EnemyMeleeAttack(Transform transform)
    {
        _transform = transform;
        _animator = _transform.GetComponent<Animator>();

    }

    public override NodeState Evaluate()
    {
        _animator.SetTrigger("Attack");

        Debug.Log("I'm in EnemyMeleeAttack");

        return NodeState.Running;

    }
}
