using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Listner : MonoBehaviour
{
    public bool indvisualResponse=false;
    public GameEvent Event;
    public UnityEvent Response;
    
    private void OnEnable()
    {
        Event.AddListener(this);
    }
    private void OnDisable()
    {
        Event.RemoveListener(this);
    }

    public void EventRaised()
    {
        Response.Invoke();

    }



}
