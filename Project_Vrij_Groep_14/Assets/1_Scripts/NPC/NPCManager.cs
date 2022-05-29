using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour {

    private List<NPCController> npcs;

    public void OnStart() {
        npcs = new List<NPCController>(FindObjectsOfType<NPCController>());
        for(int i = 0; i < npcs.Count; i++) {
            npcs[i].OnStart();
        }
    }

    public void OnUpdate() {
        for(int i = 0; i < npcs.Count; i++) {
            npcs[i].OnUpdate();
        }
    }
}