using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal_Hub : MonoBehaviour
{
    public string playerTag = "Player";

    public string SceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}
