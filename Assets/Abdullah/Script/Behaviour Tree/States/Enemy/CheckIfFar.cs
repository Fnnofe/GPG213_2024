using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckIfFar : TreeNode
{
    public float despawnDistance=55f;
    public float distancefromPLayer=2.50f;
    Transform _transform;
    Transform player;
    public CheckIfFar(Transform transform)
    {
        _transform = transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }
    public override NodeState Evaluate()
    {
        float currentDistanceFromTarget = Vector3.Distance(player.position, _transform.position);
        // if the player
        if (distancefromPLayer <= currentDistanceFromTarget && despawnDistance >= currentDistanceFromTarget)
        {

            //Chase

            return NodeState.Sucess;

        }
        else if (despawnDistance< currentDistanceFromTarget)
        {

            //TooFar 
            //despawn
            _transform.gameObject.SetActive(false);
            return NodeState.Failure;

        }


        return NodeState.Failure;


    }
}
