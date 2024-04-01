using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class DialogManager : MonoBehaviour
{
    public GameObject dialogUI;
    public Image uIImage;

    public TextMeshProUGUI nPCName;
    public TextMeshProUGUI nPCDialog;


    int dialogCount =0;

    //arange data
    Stack<DialogInfo> nextDialog;
    Stack<DialogInfo> previousDialog;

    //I need a stack here not Queue
    DialogInfo currentDialog;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.KeypadEnter)|| Input.GetKeyDown(KeyCode.Escape))
        {
             NextDialog();
        }
    }
    //starting a new conversation;
    public void StartSenario( DialogInfo[] Senario)
    {
        dialogUI.SetActive(true);

        nextDialog = new Stack<DialogInfo>(Senario.Length);
        previousDialog = new Stack<DialogInfo>(Senario.Length);

        for (int i = Senario.Length; i > 0; --i)
        {
            nextDialog.Push(Senario[i-1]);
        }
        currentDialog = nextDialog.Pop();
        //(we started the First Dialog[])
        AssignInfo();
    }

    void NextDialog()
    {
        // check if there is no more dialog and no more text to display.(we excusted Dialog[] & text[])
        if (nextDialog.Peek() == null && dialogCount == currentDialog.textOrderOfAppearing.Length - 1)
        {
            EndDialog();
        }
        else
        {
            //check if the current Dialog is exhausted to jump to the next.(we are in the next Dialog[])
            if (dialogCount >= currentDialog.textOrderOfAppearing.Length-1)
            {
                dialogCount = 0;
                previousDialog.Push(currentDialog);
                currentDialog = nextDialog.Pop();
                AssignInfo();
            }
            //check the next text to display & assign speaker's data.(we are in the current Dialog[])
            else if (dialogCount < currentDialog.textOrderOfAppearing.Length)
            {
                ++dialogCount;
                AssignInfo();
            }
        }

    }


    public void ButtonPrevious()
    {
        // we are at the first Daillog line(we are in the Dialog[0] text[0])
        if (previousDialog.Peek() == null&& currentDialog.textOrderOfAppearing[0]== currentDialog.textOrderOfAppearing[dialogCount])
        {
            return;
        }
        else
        {
            //case more than length of the text (we are in the next Dialog[])
            if (dialogCount > 0)
            {
                --dialogCount;
                AssignInfo();
            }
            else if (dialogCount == 0)
            {
                nextDialog.Push(currentDialog);
                currentDialog = previousDialog.Pop();
                dialogCount = currentDialog.textOrderOfAppearing.Length-1;
                AssignInfo();

            }
        }

    }
    private void AssignInfo()
    {
        //Speaker
            //Sprite
        uIImage.sprite = currentDialog.speakerImage;
            //name
        nPCName.text = currentDialog.speakerName;
        nPCName.color = currentDialog.speakerColor;


        //text
        if (dialogCount== currentDialog.textOrderOfAppearing.Length)
        {
            nPCDialog.text = currentDialog.textOrderOfAppearing[dialogCount-1];

        }
             else
        {
            nPCDialog.text = currentDialog.textOrderOfAppearing[dialogCount];
        }

    }

    public void ButtonNext()
    {
        NextDialog();

    }
    public void EndDialog()
    {
        dialogUI.SetActive(false);

    }

}
