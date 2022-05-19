using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {

    private FSM fsm;

    public void OnStart() {
        fsm = new FSM(typeof(IdleState),GetComponents<BaseState>());
    }

    public void OnUpdate() {
        fsm.OnUpdate();
    }
    
}