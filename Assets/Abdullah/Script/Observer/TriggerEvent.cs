using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    [Header("Optional")]
    public Listner triggerIndividually;
    [Header("Needed")]
    public GameEvent GameEvent;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            TriggerResponse();

        }
    }
    public void TriggerResponse()
    {
        Debug.Log("Triggered");
        GameEvent.Raise(triggerIndividually);

    }

}
