using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenarioInfo : MonoBehaviour
{

    public DialogInfo[] orderOfAppearing;
    public void StartSenario()
    {
        DialogManager manager=FindAnyObjectByType<DialogManager>();
        manager.StartSenario( orderOfAppearing);
    }


}
