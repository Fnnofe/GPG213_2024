using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenarioInfo : MonoBehaviour
{

    public DailogInfo[] orderOfAppearing;
    private void Start()
    {
        DailogManager manager=FindAnyObjectByType<DailogManager>();
        manager.StartSenario( orderOfAppearing);
    }


}
