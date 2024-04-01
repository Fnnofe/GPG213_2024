using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenarioInfo : MonoBehaviour
{

    public DialogInfo[] orderOfAppearing;
    private void Start()
    {
        DialogManager manager=FindAnyObjectByType<DialogManager>();
        manager.StartSenario( orderOfAppearing);
    }


}
