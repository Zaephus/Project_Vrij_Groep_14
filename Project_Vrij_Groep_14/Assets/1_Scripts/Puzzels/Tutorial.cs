using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : Puzzle
{
    private void OnEnable()
    {
        FindObjectOfType<OverworldCamera>().OnPhotoCameraPickedUp += Tutorial_OnPhotoCameraPickedUp;
    }
    private void OnDisable()
    {
        FindObjectOfType<OverworldCamera>().OnPhotoCameraPickedUp -= Tutorial_OnPhotoCameraPickedUp;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void Tutorial_OnPhotoCameraPickedUp(object sender, System.EventArgs e)
    {
        Debug.Log("je hebt me opgepakt");
        //dingen die moeten gebeuren nadat je de camera op hebt gepakt
    }

    void NextDialogue()
    {

    }
}
