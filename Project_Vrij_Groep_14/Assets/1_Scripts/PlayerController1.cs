using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour {

    public float gravity = -9.81f;
    public float rotationSpeed = 2;
    public float rotationLimitX = 45;

    [HideInInspector] public Vector3 velocity;
    public float speed = 3;
    [HideInInspector] public bool onGround;
    private float rotationX;

    private CharacterController controller;
    public Camera playerCamera;

    public void Start() {

        controller = GetComponent<CharacterController>();

    }

    public void Update() {

        if (IsOnGround() && velocity.y < 0) {
            velocity.y = 0;
        }

        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        controller.Move(move * Time.deltaTime * speed);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        rotationX += -Input.GetAxis("Mouse Y") * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, -rotationLimitX, rotationLimitX);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * rotationSpeed, 0);

    }

    public bool IsOnGround() {

        float radius = 0.09f;
        LayerMask terrainMask = LayerMask.GetMask("Terrain");

        return Physics.CheckSphere(this.transform.position, radius, terrainMask);

    }

}