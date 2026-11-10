using UnityEngine;
using UnityEngine.InputSystem;

public class UnityPlayerController : MonoBehaviour
{
    public float playerSpeed = 5f;
    public float rotationSpeed = 720f;
    public float jumpHeight = 1.5f;
    public float gravityValue = -9.81f;

    public Transform movementCamera;
    public InputActionReference moveAction; // expects Vector2
    public InputActionReference jumpAction; // expects Button

    private CharacterController controller;
    private Animator animator;
    private float verticalSpeed;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
    }

    void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
    }

    private void FixedUpdate()
    {   // How to use manul input (GetKeyDown Approach)
        if(Input.GetKeyDown(KeyCode.Q)){
            Debug.Log("Q down");
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            Debug.Log("Q up");
        }
    }

    void Update()
    {
        // 1. Read movement input (x: left/right, y: forward/backward).
        Vector2 input = moveAction.action.ReadValue<Vector2>();

        // 2. Use the camera's forward and right directions to move along the ground.
        Vector3 forward = movementCamera.forward;
        Vector3 right = movementCamera.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 move = right * input.x + forward * input.y;
        // Prevent faster movement along diagonals.
        move = Vector3.ClampMagnitude(move, 1f);

        // 3. Rotate the character toward the movement direction.
        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // 4. Reset falling speed when grounded and handle jumping.
        bool isGrounded = controller.isGrounded;
        if (isGrounded && verticalSpeed < 0f)
        {
            verticalSpeed = -2f;
        }

        if (isGrounded && jumpAction.action.WasPressedThisFrame())
        {
            verticalSpeed = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
        }

        // 5. Apply gravity, then combine horizontal and vertical movement.
        verticalSpeed += gravityValue * Time.deltaTime;
        Vector3 velocity = move * playerSpeed;
        velocity.y = verticalSpeed;
        controller.Move(velocity * Time.deltaTime);

        // Stop rising when the character hits a ceiling.
        if ((controller.collisionFlags & CollisionFlags.Above) != 0 && verticalSpeed > 0f)
        {
            verticalSpeed = 0f;
        }

        // 6. Play the walking animation while moving on the ground.
        //animator.SetBool("IsMoving", move != Vector3.zero && isGrounded);
    }
}
