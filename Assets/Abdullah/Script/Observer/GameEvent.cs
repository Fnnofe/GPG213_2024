using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "CustomEvent", menuName = "CustomEvent", order = 1)]
public class GameEvent : ScriptableObject
{
    private List<Listner> listeners = new List<Listner>();


    public void Raise(Listner _gameobject)
    {
        if (_gameobject != null && _gameobject.indvisualResponse == true)
        {
                if (_gameobject == null) return;
                if (listeners.Contains(_gameobject) ==true)
                {
                    Debug.Log("found a matching name");
                _gameobject.EventRaised();
                    return;

                }
            Debug.LogWarning("---> " + _gameobject.name + " < ---" + " Not triggering because it's not in the Event list of ---> " + this.name+ " <---");
            Debug.LogWarning("---> " + _gameobject.name + " < ---" + " is part of ---> " + _gameobject.Event.name + " <---");

        }

        else if(_gameobject == null || _gameobject.indvisualResponse == false)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                Debug.Log("Listenr Loop_"+i);
                listeners[i].EventRaised();
            }



        }

    }

    public void AddListener(Listner listner)
    {
        listeners.Add(listner);
    }
    public void RemoveListener(Listner listner)
    {
        listeners.Remove(listner);
    }


}
