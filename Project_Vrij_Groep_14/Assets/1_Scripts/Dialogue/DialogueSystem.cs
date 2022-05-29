using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour {

    public Image dialoguePanel;
    public TMP_Text dialogueText;

    private DialogueOption currentDialogueOption;

    public float delayBeforeStart = 0f;
    public float timeBetweenChars = 0.1f;

    public void Initialize(DialogueOption d) {

        currentDialogueOption = d;
        StartCoroutine(TypeWriter(currentDialogueOption.dialogue));

        dialoguePanel.enabled = true;
        dialogueText.text = "";

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
        //hier iets doen om door te gaan naar de volgende dialoog optie
    }

    public IEnumerator TypeWriter(string dialogue) {

        yield return new WaitForSeconds(delayBeforeStart);

        foreach(char c in dialogue) {
            //...
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(0.35f);
        ContinueDialogue();

    }

    public void EndDialogue() {
        dialoguePanel.enabled = false;
    }

}