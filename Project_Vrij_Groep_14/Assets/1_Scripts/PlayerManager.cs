using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public PlayerController playerController;

    public void Start() {
        playerController.OnStart();
    }

    public void Update() {
        playerController.OnUpdate();
    }
}