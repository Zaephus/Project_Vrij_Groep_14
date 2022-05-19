using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour {

    private List<NPCController> npcs;

    public void Start() {
        npcs = new List<NPCController>(FindObjectsOfType<NPCController>());
        for(int i = 0; i < npcs.Count; i++) {
            npcs[i].OnStart();
        }
    }

    public void Update() {
        for(int i = 0; i < npcs.Count; i++) {
            npcs[i].OnUpdate();
        }
    }
}