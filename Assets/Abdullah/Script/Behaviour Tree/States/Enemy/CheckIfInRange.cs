using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using static UnityEngine.EventSystems.EventTrigger;

public class CheckIfInRange : TreeNode
{
    [SerializeField] float attackSpeed;
    Transform _transform, player;
    public float distancefromPLayer = 3;
    float chaseTimer;
    Entity entity;
    public CheckIfInRange(Transform transform)
    {
        _transform = transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        entity= transform.GetComponent<Entity>();
    }
    public override NodeState Evaluate()
    {
        float currentDistanceFromTarget = Vector3.Distance(player.position, _transform.position);
        if (currentDistanceFromTarget <= distancefromPLayer)
        {
         //   entity.enabled = false;
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
