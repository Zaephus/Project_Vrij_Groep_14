using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : Puzzle
{

    public GameObject stainedGlassPiecePrefab;

    public NPCController nun;

    public DialogueOption cameraPrompt;
    public DialogueOption stainedGlassPrompt;
    public DialogueOption placedTokenPrompt;

    public event EventHandler IsSolved;

    private void OnEnable()
    {
        FindObjectOfType<OverworldCamera>().OnPhotoCameraPickedUp += Tutorial_OnPhotoCameraPickedUp;
        FindObjectOfType<PhotoCapture>().OnTakePicture += Tutorial_OnTakePicture;
        nun.OnInteract += Tutorial_OnTalkToNun;

        FindObjectOfType<StainedGlassHub>().OnFirstToken += Tutorial_OnPlaceToken;

    }

    void Tutorial_OnTalkToNun(object sender, System.EventArgs e) 
    {
        FindObjectOfType<OverworldCamera>().canInteract = true;
        nun.OnInteract -= Tutorial_OnTalkToNun;
    }

    void Tutorial_OnPhotoCameraPickedUp(object sender, System.EventArgs e)
    {
        FindObjectOfType<MenuManager>().gameState = MenuManager.GameState.Pause;
        StartCoroutine(StartPrompt(cameraPrompt));
    }

    void Tutorial_OnTakePicture(object sender, System.EventArgs e)
    {
        PlayerManager player = FindObjectOfType<PlayerManager>();
        GameObject stainedGlassPuck = Instantiate(stainedGlassPiecePrefab,player.playerInteract.holdTransform.position,player.playerInteract.holdTransform.rotation);
        player.playerInteract.canInteract = false;
        stainedGlassPuck.GetComponent<StainedGlassPiece>().isHeld = true;
        player.playerInteract.holdItem = stainedGlassPuck;
        player.playerInteract.dropable = stainedGlassPuck.GetComponent<IDropable>();
        player.playerInteract.isHolding = true;
        player.playerInteract.canInteract = false;
        FindObjectOfType<PhotoCapture>().OnTakePicture -= Tutorial_OnTakePicture;

        StartCoroutine(StartPrompt(stainedGlassPrompt));
    }

    void Tutorial_OnPlaceToken(object sender,System.EventArgs e) {

        StartCoroutine(StartPrompt(placedTokenPrompt));
        IsSolved?.Invoke(this,EventArgs.Empty);

        FindObjectOfType<StainedGlassHub>().OnFirstToken -= Tutorial_OnPlaceToken;
    }

    public IEnumerator StartPrompt(DialogueOption prompt) {
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<DialogueSystem>().Initialize(prompt,"");
    }

    void NextDialogue()
    {

    }
}