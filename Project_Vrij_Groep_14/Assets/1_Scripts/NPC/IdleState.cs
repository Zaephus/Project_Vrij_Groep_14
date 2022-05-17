using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState {

    private NPCController npc;

    public float waitDuration = 1;
    private float timer = 0;

    public override void OnEnter() {
        npc = GetComponent<NPCController>();
        timer = waitDuration;
    }

    public override void OnUpdate() {
        timer -= Time.deltaTime;
        if(timer <= 0) {
            owner.SwitchState(typeof(PatrolState));
        }
    }

    public override void OnExit() {
        timer = 1000;
    }

}
