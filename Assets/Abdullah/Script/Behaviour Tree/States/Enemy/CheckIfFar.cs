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
            Debug.Log("not too far");

            return NodeState.Sucess;

        }
        else if (despawnDistance< currentDistanceFromTarget)
        {

            //TooFar 
            //despawn
            Debug.Log("DespawnEnemy");
            player.gameObject.SetActive(false);
            return NodeState.Failure;

        }

        Debug.Log("too close");

        return NodeState.Failure;


    }
}
