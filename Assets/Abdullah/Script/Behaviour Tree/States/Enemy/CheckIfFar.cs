using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using BehaviorTree;

public class CheckIfFar : TreeNode
{
    public float despawnDistance;
    public float distancefromPLayer;
    Transform _transform;
    Transform player;
    public CheckIfFar(Transform transform)
    {
        _transform = transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }
    public override NodeState Evaluate()
    {
        Debug.Log(player.name + "I'm faaaaaaaaaaaar " + _transform.name);
        float currentDistanceFromTarget = Vector3.Distance(player.position, _transform.position);
        // if the player
        if (currentDistanceFromTarget >= distancefromPLayer && currentDistanceFromTarget < despawnDistance)
        {

            //Chase
            return NodeState.Sucess;

        }
        else if (currentDistanceFromTarget >= despawnDistance){

            //TooFar 
            return NodeState.Failure;

        }


        return NodeState.Sucess;


    }
}
