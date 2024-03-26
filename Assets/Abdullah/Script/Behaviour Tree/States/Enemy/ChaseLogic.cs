using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class ChaseLogic : TreeNode
{
    public float distanceToPLayer;
    private Transform _transform;
    private Transform player;
    private Animation _animation;
    private Unit _unit;
    RaycastHit hit;

    public ChaseLogic(Transform transform)
    {
        _transform = transform;
        _animation = transform.GetComponent<Animation>();
        _unit = transform.GetComponent<Unit>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
   

    public override NodeState Evaluate()
        {

        if ("help" == player.name)
        {
            _unit.FindPath();
            Debug.Log("I can see the Player");
            return NodeState.Sucess;

        }
        else Debug.Log("Player is Hidding");
        return NodeState.Sucess;

        }
    



    }


