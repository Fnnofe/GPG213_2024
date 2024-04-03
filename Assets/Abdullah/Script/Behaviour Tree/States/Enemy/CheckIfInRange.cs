using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckIfInRange : TreeNode
{
    [SerializeField] float attackSpeed;
    Transform _transform, player;
    public float distancefromPLayer = 10.00f;
    float chaseTimer;

    public CheckIfInRange(Transform transform)
    {
        _transform = transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public override NodeState Evaluate()
    {
        float currentDistanceFromTarget = Vector3.Distance(player.position, _transform.position);
        if (currentDistanceFromTarget <= distancefromPLayer)
        {
            Debug.Log("______check if in range SUCESS______");
            return NodeState.Sucess;
        }
        //TooFar
        else
        {

            return NodeState.Failure;
        }
    }

}
