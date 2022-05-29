using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    private PlayerManager playerManager;
    private NPCManager npcManager;
    private DialogueSystem dialogueSystem;

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
}