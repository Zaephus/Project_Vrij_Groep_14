using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : Puzzle
{

    public GameObject stainedGlassPiecePrefab;

    public NPCController nun;

    public DialogueOption prompt;

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
    }

    void Tutorial_OnTalkToNun(object sender, System.EventArgs e) 
    {
        FindObjectOfType<OverworldCamera>().canInteract = true;
        nun.OnInteract -= Tutorial_OnTalkToNun;
    }

    void Tutorial_OnPhotoCameraPickedUp(object sender, System.EventArgs e)
    {
        FindObjectOfType<MenuManager>().gameState = MenuManager.GameState.Pause;
        FindObjectOfType<DialogueSystem>().Initialize(prompt,"");
        //StartCoroutine(StartPrompt(prompts[0]));
        Debug.Log("je hebt me opgepakt");
        //dingen die moeten gebeuren nadat je de camera op hebt gepakt
    }

    void Tutorial_OnLookThroughCamera(object sender, System.EventArgs e)
    {
        //StartCoroutine(StartPrompt(prompts[1]));
        Debug.Log("Door camera gekeken joepie");
    }

    void Tutorial_OnTakePicture(object sender, System.EventArgs e)
    {
        PlayerManager player = FindObjectOfType<PlayerManager>();
        GameObject stainedGlassPuck = Instantiate(stainedGlassPiecePrefab,player.playerInteract.holdTransform.position,player.playerInteract.holdTransform.rotation,player.playerInteract.holdTransform);
        stainedGlassPuck.GetComponent<StainedGlassPiece>().isHeld = true;
        player.playerInteract.dropable = stainedGlassPuck.GetComponent<IDropable>();
        player.playerInteract.isHolding = true;
        FindObjectOfType<PhotoCapture>().OnTakePicture -= Tutorial_OnTakePicture;
        Debug.Log("Foto genomen");
    }

    public IEnumerator StartPrompt(DialogueOption prompt) {
        yield return new WaitForSeconds(0.15f);
        FindObjectOfType<DialogueSystem>().Initialize(prompt,"");
    }

    void NextDialogue()
    {

    }
}