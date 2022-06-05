using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueOption",menuName = "DialogueOption")]
public class DialogueOption : ScriptableObject {

    [TextArea(15,20)]
    public string dialogue;
    public DialogueOption nextDialogueOption;

    public bool pauseBreak;
    public bool endBreak;

}