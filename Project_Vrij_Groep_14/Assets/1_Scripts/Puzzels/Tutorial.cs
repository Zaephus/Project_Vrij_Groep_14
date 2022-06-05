using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : Puzzle
{

    public NPCController nun;

    private void OnEnable()
    {
        FindObjectOfType<OverworldCamera>().OnPhotoCameraPickedUp += Tutorial_OnPhotoCameraPickedUp;
        FindObjectOfType<PhotoCapture>().OnLookThroughCamera += Tutorial_OnLookThroughCamera;
        FindObjectOfType<PhotoCapture>().OnTakePicture += Tutorial_OnTakePicture;
        nun.OnInteract += Tutorial_OnTalkToNun;

    }
    private void OnDisable()
    {
        FindObjectOfType<PhotoCapture>().OnLookThroughCamera -= Tutorial_OnLookThroughCamera;
        FindObjectOfType<PhotoCapture>().OnTakePicture -= Tutorial_OnTakePicture;
        nun.OnInteract -= Tutorial_OnTalkToNun;
    }

    void Tutorial_OnTalkToNun(object sender, System.EventArgs e) 
    {
        FindObjectOfType<OverworldCamera>().canInteract = true;
    }

    void Tutorial_OnPhotoCameraPickedUp(object sender, System.EventArgs e)
    {
        FindObjectOfType<MenuManager>().gameState = MenuManager.GameState.Pause;
        FindObjectOfType<DialogueSystem>().ContinueDialogue();
        Debug.Log("je hebt me opgepakt");
        //dingen die moeten gebeuren nadat je de camera op hebt gepakt
    }

    void Tutorial_OnLookThroughCamera(object sender, System.EventArgs e)
    {
        Debug.Log("Door camera gekeken joepie");

    }

    void Tutorial_OnTakePicture(object sender, System.EventArgs e)
    {
        Debug.Log("Foto genomen");
    }

    void NextDialogue()
    {

    }
}