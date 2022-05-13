using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public PlayerController playerController;
    public PlayerInteract playerInteract;

    public void Start() {
        playerController.OnStart();
        playerInteract.OnStart(this);
    }

    public void Update() {
        playerController.OnUpdate();
        playerInteract.OnUpdate();
    }
}