using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "CustomEvent", menuName = "CustomEvent", order = 1)]
public class GameEvent : ScriptableObject
{
    private List<Listner> listeners = new List<Listner>();


    public void Raise(GameObject gameobject,bool indivsual)
    {
        if (indivsual == false)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                if(gameobject.name== listeners[i].name)
                listeners[i].EventRaised();


            }
        }
        else if (indivsual == true)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
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
