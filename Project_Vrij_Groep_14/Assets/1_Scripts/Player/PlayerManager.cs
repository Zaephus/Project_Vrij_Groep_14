using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public PlayerController playerController;
    public PlayerInteract playerInteract;

    public void OnStart() {
        playerController.OnStart();
        playerInteract.OnStart(this);
    }

    public void OnUpdate() {
        playerController.OnUpdate();
        playerInteract.OnUpdate();
    }
}