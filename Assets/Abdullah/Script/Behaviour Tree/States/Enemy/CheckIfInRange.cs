using UnityEngine;

using BehaviorTree;

public class CheckIfInRange : TreeNode
{
    [SerializeField] float attackSpeed;
    Transform _transform, player;
    public float distancefromPLayer = 3;
    BoidsBehaviour boidsBehaviour;
    float chaseSpeed;
    public CheckIfInRange(Transform transform)
    {
        _transform = transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        boidsBehaviour=_transform.GetComponent<BoidsBehaviour>();
        chaseSpeed = boidsBehaviour.chaseWeight;
    }
    public override NodeState Evaluate()
    {
        
        float currentDistanceFromTarget = Vector3.Distance(player.position, _transform.position);
        if (currentDistanceFromTarget <= distancefromPLayer)
        {
            boidsBehaviour.chaseWeight = 0;
                return NodeState.Sucess;
        }
        //TooFar
        else
        {
            boidsBehaviour.chaseWeight = chaseSpeed;

            return NodeState.Failure;
        }
    }

}
