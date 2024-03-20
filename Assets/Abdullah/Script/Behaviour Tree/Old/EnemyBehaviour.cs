using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//                                                  Genearic type T
public class EnemyBehaviour : BaseNode<EnemyBehaviour.EnemyStates>
{
    public EnemyBehaviour(EnemyStates key) : base(key)
    {
    }

    //My concreate state.
    public enum EnemyStates
    {
        Melee,
        Range,
        InRange,
        Pathfinding,
        Dead,


    }

    void Start()
    {

    }

    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }

    public override EnemyStates GetNextState()
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerEnter(Collider collision)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerStay(Collider collision)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerExit(Collider collision)
    {
        throw new System.NotImplementedException();
    }

}
