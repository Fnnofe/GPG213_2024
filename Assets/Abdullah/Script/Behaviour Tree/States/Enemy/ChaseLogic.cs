using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using static UnityEngine.UI.CanvasScaler;

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
        Physics.Raycast(_transform.position, player.position, out hit);
        Debug.Log("-----RayCast HIT:" + hit.transform.name+ "-----");

        if (hit.transform.name == player.name)
        {
            _unit.FindPath();
            Debug.Log("I can see the Player");
            return NodeState.Sucess;
        }
        return NodeState.Failure;
        }
    



    }


