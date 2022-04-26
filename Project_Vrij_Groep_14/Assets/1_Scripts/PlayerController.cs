using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float gravity = -9.81f;

    [HideInInspector] public Vector3 velocity;
    public float speed = 3;

    private CharacterController controller;

    public void Start() {
        controller = GetComponent<CharacterController>();
    }

    public void GetInput() {
        
    }

    public void Update() {

        if(IsOnGround() && velocity.y < 0) {
            velocity.y = -1f;
        }


    }

    public bool IsOnGround() {
        float radius = 0.09f;
        LayerMask terrainMask = LayerMask.GetMask("Terrain");

        return Physics.CheckSphere(this.transform.position,radius,terrainMask);
    }
}