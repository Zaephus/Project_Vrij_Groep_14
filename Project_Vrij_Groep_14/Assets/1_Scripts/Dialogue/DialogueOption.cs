using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueOption",menuName = "DialogueOption")]
public class DialogueOption : ScriptableObject {

    public string dialogue;
    public DialogueOption nextDialogueOption;

    public bool pauseBreak;
    public bool endBreak;

}