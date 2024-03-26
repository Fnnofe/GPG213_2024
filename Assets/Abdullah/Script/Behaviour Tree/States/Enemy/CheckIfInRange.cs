using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using BehaviorTree;

public class CheckIfInRange : TreeNode
{
    [SerializeField] float attackSpeed;
    Transform _transform,player;
    public float distancefromPLayer=5;
    public CheckIfInRange(Transform transform)
    {
        _transform = transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("I'm in CheckIfInRange");
        float currentDistanceFromTarget = Vector3.Distance(player.position, _transform.position);
        // if the player
        if (currentDistanceFromTarget <= distancefromPLayer )
        {

            //Chase
            Debug.Log("I'm close");
            return NodeState.Sucess;

        }
        //TooFar
        else
            Debug.Log("I'm far");
        return NodeState.Failure;

    }

}
