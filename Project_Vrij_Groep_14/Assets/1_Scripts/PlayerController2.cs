using UnityEngine;

public class PlayerController2 : MonoBehaviour {
    float sidewaysInput;
    float forwardInput;
    bool isGrounded;
    Vector3 playerVelocity;
    float gravityValue = -9.81f;

    [Header("Stats")]
    [SerializeField]
    float jumpHeight;
    [SerializeField]
    float walkSpeed;
    [SerializeField]
    float runSpeed;

    private enum StateEnum { Idle, Walk, Run, Jump }
    private StateEnum state = StateEnum.Walk;

    [Header("Components")]
    public CharacterController controller;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private void CheckState() {
        switch (state) {
            case StateEnum.Walk: WalkBehaviour(); break;
            case StateEnum.Run: RunBehaviour(); break;
            case StateEnum.Jump: JumpBehaviour(); break;
        }
    }

    // Update is called once per frame
    public void OnUpdate() {
        GetInput();
        CheckState();
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);     //groundcheck 

        if (isGrounded && playerVelocity.y < 0) {
            playerVelocity.y = -1f;
        }

        //zwaartekracht
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void GetInput() {
        sidewaysInput = Input.GetAxis("Horizontal");      //input links/rechts
        forwardInput = Input.GetAxis("Vertical");          //input voor/achter
    }


    void WalkBehaviour() {
        float playerSpeed = walkSpeed;
        Vector3 move = transform.right * sidewaysInput + transform.forward * forwardInput;      //beweging op basis van de orientatie van de speler
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (Input.GetButtonDown("Jump") && isGrounded) {
            state = StateEnum.Jump;
        }

        if (Input.GetButton("Fire3") && isGrounded) {
            state = StateEnum.Run;
        }

    }

    void RunBehaviour() {
        float playerSpeed = runSpeed;
        Vector3 move = transform.right * sidewaysInput + transform.forward * forwardInput;      //beweging op basis van de orientatie van de speler
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (Input.GetButtonDown("Jump") && isGrounded) {
            state = StateEnum.Jump;
        }

        if (Input.GetButtonUp("Fire3")) {
            state = StateEnum.Walk;
        }
    }

    void JumpBehaviour() {
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);

        if (isGrounded) {
            state = StateEnum.Walk;
        }
    }

}