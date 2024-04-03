using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerConversation : MonoBehaviour
{
    public TextMeshProUGUI NPCUi;
    public SenarioInfo ScenarioToStart;
    bool playerIsHere=false;
    DialogManager manager;

    // Start is called before the first frame update
    private void Start()
    {
         manager = FindAnyObjectByType<DialogManager>();

    }
    private void Update()
    {
        
        if (playerIsHere == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ///Trigger 
                ScenarioToStart.StartSenario();
                playerIsHere = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            NPCUi.gameObject.SetActive(true);
            playerIsHere = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            playerIsHere = false;


        }
        NPCUi.gameObject.SetActive(false);
        manager.EndDialog();
    }



}
