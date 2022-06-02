using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : Puzzle
{
    private void OnEnable()
    {
        FindObjectOfType<OverworldCamera>().OnPhotoCameraPickedUp += Tutorial_OnPhotoCameraPickedUp;
        FindObjectOfType<PhotoCapture>().OnLookThroughCamera += Tutorial_OnLookThroughCamera;
        FindObjectOfType<PhotoCapture>().OnTakePicture += Tutorial_OnTakePicture;
    }
    private void OnDisable()
    {
        FindObjectOfType<PhotoCapture>().OnLookThroughCamera -= Tutorial_OnLookThroughCamera;
        FindObjectOfType<PhotoCapture>().OnTakePicture -= Tutorial_OnTakePicture;
    }


    void Tutorial_OnPhotoCameraPickedUp(object sender, System.EventArgs e)
    {
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
