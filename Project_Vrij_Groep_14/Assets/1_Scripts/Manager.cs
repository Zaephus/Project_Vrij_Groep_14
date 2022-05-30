using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    private PlayerManager playerManager;
    private NPCManager npcManager;
    private DialogueSystem dialogueSystem;

    #region Singleton
    public static Manager instance;

    void Awake() {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    public void Start() {

        playerManager = FindObjectOfType<PlayerManager>();
        npcManager = GetComponent<NPCManager>();
        dialogueSystem = GetComponent<DialogueSystem>();

        playerManager.OnStart();
        npcManager.OnStart();

    }

    public void Update() {

        playerManager.OnUpdate();
        npcManager.OnUpdate();
        
    }

    public void StartDialogue(DialogueOption d) {
        Debug.Log("Started Dialogue from manager");
        dialogueSystem.Initialize(d);
    }
}