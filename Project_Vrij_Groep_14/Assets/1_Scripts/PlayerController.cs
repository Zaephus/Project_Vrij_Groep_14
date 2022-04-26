using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float gravity = -9.81f;

    [HideInInspector] public Vector3 velocity;

    private float playerSpeed;
    public float walkSpeed = 3;
    public float runSpeed = 6;
    public float jumpHeight = 3;

    [Range(100,200)]
    public float mouseSensitivityX = 100;
    [Range(100,200)]
    public float mouseSensitivityY = 100;
    float xRotation = 0;

    private CharacterController controller;
    public Camera playerCamera;
    public Transform playerBody;

    public void OnStart() {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnUpdate() {

        if(IsOnGround() && velocity.y < 0) {
            velocity.y = -1f;
        }

        if(Input.GetButton("Sprint")) {
            playerSpeed = runSpeed;
        }
        else {
            playerSpeed = walkSpeed;
        }

        if(Input.GetButtonDown("Jump") && IsOnGround()) {
            velocity.y += Mathf.Sqrt(jumpHeight*-3*gravity);
        }

        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        controller.Move(move * Time.deltaTime * playerSpeed);

        velocity.y += gravity *Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        Look();

    }

    public void Look() {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation,-20,45);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation,0,0);
        playerBody.Rotate(Vector3.up * mouseX);

    }

    public bool IsOnGround() {
        float radius = 0.09f;
        LayerMask terrainMask = LayerMask.GetMask("Terrain");

        return Physics.CheckSphere(this.transform.position,radius,terrainMask);
    }
}