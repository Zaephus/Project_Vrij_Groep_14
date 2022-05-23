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

    private float horizontalInput;
    private float verticalInput;

    [Range(100,200)]
    public float mouseSensitivityX = 100;
    [Range(100,200)]
    public float mouseSensitivityY = 100;
    float xRotation = 0;

    private CharacterController controller;
    public Animator animator;
    public Transform playerCamera;
    public Transform playerHead;
    public Transform playerBody;

    public void OnStart() {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnUpdate() {

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        animator.SetFloat("Forward",verticalInput);
        animator.SetFloat("Right",horizontalInput);

        if(IsOnGround() && velocity.y < 0) {
            velocity.y = -1f;
        }

        if(playerSpeed > walkSpeed) {
            animator.SetBool("IsSprinting",true);
        }
        else {
            animator.SetBool("IsSprinting",false);
        }

        if(Input.GetButton("Sprint") && (horizontalInput != 0 || verticalInput != 0)) {
            playerSpeed = runSpeed;
        }
        else {
            playerSpeed = walkSpeed;
        }

        if(Input.GetButtonDown("Jump") && IsOnGround()) {
            velocity.y += Mathf.Sqrt(jumpHeight*-3*gravity);
            animator.SetTrigger("IsJumping");
        }

        // if() {
        //     animator.SetBool("IsJumping",false);
        // }

        Move();
        Look();

    }

    public void Move() {

        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;
        controller.Move(move * Time.deltaTime * playerSpeed);

        velocity.y += gravity *Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    public void Look() {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation,-20,45);

        //playerCamera.localRotation = Quaternion.Euler(xRotation,0,0);
        //The head is on a different axis
        playerHead.localRotation = Quaternion.Euler(xRotation,0,0);
        playerBody.Rotate(Vector3.up * mouseX);

    }

    public bool IsOnGround() {
        float radius = 0.09f;
        LayerMask terrainMask = LayerMask.GetMask("Terrain");

        return Physics.CheckSphere(this.transform.position,radius,terrainMask);
    }
}