using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiderSwitch : MonoBehaviour
{
    public Collider meleeColider;
    public void DisableControll()
    {
        meleeColider.enabled = false;


    }
    public void EnableControll()
    {

        meleeColider.enabled = true;
    
    }

}
