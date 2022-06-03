using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour {

    public GameObject dialoguePanel;
    public GameObject continueButton;
    public TMP_Text dialogueText;

    private DialogueOption currentDialogueOption;

    public float delayBeforeStart = 0f;
    public float standardTimeBetweenChars = 0.1f;
    private float timeBetweenChars;

    public bool dialogueEnded;

    public void Initialize(DialogueOption d) {

        FindObjectOfType<MenuManager>().gameState = MenuManager.GameState.Pause;

        timeBetweenChars = standardTimeBetweenChars;
        
        dialoguePanel.SetActive(true);
        dialogueText.text = "";
        Debug.Log("Initialized dialoguesystem");

        currentDialogueOption = d;
        StartCoroutine(TypeWriter(currentDialogueOption.dialogue));

    }

    public void ContinueDialogue() {

        if(currentDialogueOption.endBreak) {
            EndDialogue();
        }
        else {
            currentDialogueOption = currentDialogueOption.nextDialogueOption;
            StartCoroutine(TypeWriter(currentDialogueOption.dialogue));
        }

    }

    public void OnUpdate() {

        if(dialogueEnded && !currentDialogueOption.pauseBreak) {
            continueButton.SetActive(true); 
        }
        else {
            continueButton.SetActive(false);
        }

        if(dialogueEnded && currentDialogueOption.pauseBreak) {
            FindObjectOfType<MenuManager>().gameState = MenuManager.GameState.Play;
        }

        if(Input.GetKey("g")) {
            timeBetweenChars = timeBetweenChars*0.75f;
        }
        else {
            timeBetweenChars = standardTimeBetweenChars;
        }
    
    }

    public IEnumerator TypeWriter(string dialogue) {

        dialogueText.text = "";
        dialogueEnded = false;
        yield return new WaitForSeconds(delayBeforeStart);

        foreach(char c in dialogue) {
            dialogueText.text += c;
            yield return new WaitForSeconds(timeBetweenChars);
        }

        yield return new WaitForSeconds(0.35f);
        continueButton.SetActive(true);
        dialogueEnded = true;

    }

    public void EndDialogue() {
        dialoguePanel.SetActive(false);
        FindObjectOfType<MenuManager>().gameState = MenuManager.GameState.Play;

    }

}