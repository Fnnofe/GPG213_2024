using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseNode <Estate>where Estate : Enum
{//Base State.
    
    public BaseNode(Estate key)
    {
        StateKey = key;
    }
    public Estate StateKey { get; private set; }

    public abstract void EnterState();

     public abstract void ExitState();
     public abstract void UpdateState();
    
     public abstract Estate GetNextState();
    public abstract void OnTriggerEnter(Collider collision);
     public abstract void OnTriggerStay(Collider collision);
     public abstract void OnTriggerExit(Collider collision);
}
