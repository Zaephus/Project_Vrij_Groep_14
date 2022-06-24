using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float gravity = -9.81f;

    [HideInInspector] public Vector3 velocity;

    private float playerSpeed;
    public float walkSpeed = 3;
    public float runSpeed = 6;
    public float jumpHeight = 1;

    public bool canMove = true;

    private float horizontalInput;
    private float verticalInput;

    [Range(30, 150)]
    public float mouseSensitivityX = 100;
    [Range(30, 150)]
    public float mouseSensitivityY = 100;
    float xRotation = 0;

    private CharacterController controller;
    public Animator animator;
    public Transform playerCamera;
    public Transform playerHead;
    public Transform playerBody;

    public bool onGround;

    public void OnStart()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnUpdate()
    {

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        animator.SetFloat("Forward", verticalInput);
        animator.SetFloat("Right", horizontalInput);

        if (onGround && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        if (playerSpeed > walkSpeed)
        {
            animator.SetBool("IsSprinting", true);
        }
        else
        {
            animator.SetBool("IsSprinting", false);
        }

        if (Input.GetButton("Sprint") && (horizontalInput != 0 || verticalInput != 0))
        {
            playerSpeed = runSpeed;
        }
        else
        {
            playerSpeed = walkSpeed;
        }

        if (Input.GetButtonDown("Jump") && onGround && canMove)
        {
            velocity.y = Mathf.Sqrt(jumpHeight);
            animator.SetTrigger("IsJumping");
        }

        if (canMove)
        {
            Move();
            Look();

        }

    }

    public void Move()
    {

        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;
        controller.Move(move * Time.deltaTime * playerSpeed);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    public void Look()
    {

        float mouseX = Input.GetAxis("Mouse X") * (mouseSensitivityX/100);
        float mouseY = Input.GetAxis("Mouse Y") * (mouseSensitivityY/100);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45, 60);

        //playerCamera.localRotation = Quaternion.Euler(xRotation,0,0);
        //The head is on a different axis
        playerHead.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);

    }

    public void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Ground") {
            onGround = true;
        }
    }

    public void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Ground") {
            onGround = false;
        }
    }

}