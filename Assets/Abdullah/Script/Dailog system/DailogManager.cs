using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class DailogManager : MonoBehaviour
{
    public GameObject dailogUI;
    public Image uIImage;

    public TextMeshProUGUI nPCName;
    public TextMeshProUGUI nPCDailog;


    int dailogCount =0;

    public static DailogManager instance;

    SenarioInfo collectedSenarios;
    //arange data
    Stack<DailogInfo> nextDailog;
    Stack<DailogInfo> previousDailog;

    //I need a stack here not Queue
    DailogInfo currentDailog;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.KeypadEnter)|| Input.GetKeyDown(KeyCode.Escape))
        {
             NextDailog();
        }
    }
    //starting a new conversation;
    public void StartSenario( DailogInfo[] Senario)
    {
        dailogUI.SetActive(true);

        nextDailog = new Stack<DailogInfo>(Senario.Length);
        previousDailog = new Stack<DailogInfo>(Senario.Length);

        for (int i = Senario.Length; i > 0; --i)
        {
            nextDailog.Push(Senario[i-1]);
        }
        currentDailog = nextDailog.Pop();
        //(we started the First Dailog[])
        AssignInfo();
    }

    void NextDailog()
    {
        // check if there is no more dailog and no more text to display.(we excusted Dailog[] & text[])
        if (nextDailog.Peek() == null && dailogCount == currentDailog.textOrderOfAppearing.Length - 1)
        {
            EndDailog();
        }
        else
        {
            //check if the current Dailog is exhausted to jump to the next.(we are in the next Dailog[])
            if (dailogCount >= currentDailog.textOrderOfAppearing.Length-1)
            {
                dailogCount = 0;
                previousDailog.Push(currentDailog);
                currentDailog = nextDailog.Pop();
                AssignInfo();
            }
            //check the next text to display & assign speaker's data.(we are in the current Dailog[])
            else if (dailogCount < currentDailog.textOrderOfAppearing.Length)
            {
                ++dailogCount;
                AssignInfo();
            }
        }

    }


    public void ButtonPrevious()
    {
        // we are at the first Daillog line(we are in the Dailog[0] text[0])
        if (previousDailog.Peek() == null&& currentDailog.textOrderOfAppearing[0]== currentDailog.textOrderOfAppearing[dailogCount])
        {
            return;
        }
        else
        {
            //case more than length of the text (we are in the next Dailog[])
            if (dailogCount > 0)
            {
                --dailogCount;
                AssignInfo();
            }
            else if (dailogCount == 0)
            {
                nextDailog.Push(currentDailog);
                currentDailog = previousDailog.Pop();
                dailogCount = currentDailog.textOrderOfAppearing.Length-1;
                AssignInfo();

            }
        }

    }
    private void AssignInfo()
    {
        //Speaker
            //Sprite
        uIImage.sprite = currentDailog.speakerImage;
            //name
        nPCName.text = currentDailog.speakerName;
        nPCName.color = currentDailog.speakerColor;


        //text
        if (dailogCount== currentDailog.textOrderOfAppearing.Length)
        {
            nPCDailog.text = currentDailog.textOrderOfAppearing[dailogCount-1];

        }
             else
        {
            nPCDailog.text = currentDailog.textOrderOfAppearing[dailogCount];
        }

    }

    public void ButtonNext()
    {
        NextDailog();

    }
    public void EndDailog()
    {
        dailogUI.SetActive(false);

    }

}
