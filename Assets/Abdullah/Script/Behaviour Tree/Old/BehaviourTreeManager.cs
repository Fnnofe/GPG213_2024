using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourTreeManager<Estate> : MonoBehaviour where Estate :Enum
{
    //create a new states group to manage.
   protected Dictionary<Estate, BaseNode<Estate>>States= new Dictionary<Estate, BaseNode<Estate>>();
   protected BaseNode<Estate> CurrentState;
   protected bool isTransitioningState = false;

    //state manager
    void Start()
    {
        CurrentState.EnterState();
    }
    void Update()
    {
        Estate nextStateKey= CurrentState.GetNextState();
        if(!isTransitioningState && nextStateKey.Equals(CurrentState.StateKey))
        {
            CurrentState.UpdateState();

        }
        else if(!isTransitioningState)
        {
            TransitionToState(nextStateKey);
        }
    }
    void TransitionToState(Estate stateKey) //evaluate ?
    {
        //making sure when it's called on multiple frames it will not mess up which state it's in.
        isTransitioningState = true;
        CurrentState.ExitState();
        CurrentState = States[stateKey];
        CurrentState.EnterState();
        isTransitioningState=false;

    }
    void OnTriggerEnter(Collider other)
    {
        CurrentState.OnTriggerEnter(other);
    }
    void OnTriggerStay(Collider other)
    {
        CurrentState.OnTriggerStay(other);
    }

    void OnTriggerExit(Collider other)
    {
        CurrentState.OnTriggerExit(other);
    }

}
