using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public PlayerController playerController;
    public PlayerInteract playerInteract;
    public Camera playerCamera;

    public bool hasCamera = false;

    [HideInInspector] public Animator animator;

    public void OnStart() {
        playerController.OnStart();
        animator = playerController.animator;
        playerInteract.OnStart(this);
    }

    public void OnUpdate() {
        playerController.OnUpdate();
        playerInteract.OnUpdate();
    }
}