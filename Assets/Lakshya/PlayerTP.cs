using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTP : MonoBehaviour
{
    public Transform teleportPosition;
    public GameObject player;

    public void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        other.transform.position = teleportPosition.position;
        Debug.Log("TP TIME");
        Destroy(gameObject);
    }

}
}