using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using BehaviorTree;

public class EnemyCastAttack : TreeNode
{
    [SerializeField] float attackSpeed;
    Transform _transform;
    Animator _animator;

    public EnemyCastAttack(Transform transform)
    {
        _transform = transform;
        _animator = _transform.GetComponent<Animator>();

    }

    public override NodeState Evaluate()
    {
       // _animator.SetTrigger("Attack");

        Debug.Log("I'm in EnemyCastAttack");

        return NodeState.Running;

    }
}
