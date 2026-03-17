using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    public float playerSpeed = 5.0f;


    public float jumpForce = 12f;
    public float gravity = -25f;
    public float fallMultiplier = 2.2f;
    public float lowJumpMultiplier = 2f;
    public int maxJumps = 1;

    private int jumpCount;
    private float yVelocity;

    private Vector3 move;

    public CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private bool hanging;
    private bool canMove = true;

    public float forwardDistance;

    private Transform cameraTransform;

    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    [Header("Input Actions")]
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    

    private void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
    }

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer)
        {
            coyoteTimeCounter = coyoteTime;

            if (yVelocity < -2f)
            {
                yVelocity = -2f;
            }

            jumpCount = 0;
        }

        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Read input
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        move = new Vector3(input.x, 0, input.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;
        move = Vector3.ClampMagnitude(move, 1f);

        // Jump using WasPressedThisFrame()
        if (hanging && jumpAction.action.WasPressedThisFrame())
        {
            hanging = false;
            StartCoroutine(EnableCanMove(0.25f));

            yVelocity = jumpForce;
            jumpCount++;

        }

        else if (jumpAction.action.WasPressedThisFrame() && (jumpCount < maxJumps || coyoteTimeCounter > 0f))
        {
            yVelocity = jumpForce;
            jumpCount++;
            coyoteTimeCounter =0f;
            SoundManager.PlaySound(SoundType.SHORTJUMP, 1);
        }

        if (yVelocity < 0)
        {
            yVelocity += gravity * fallMultiplier * Time.deltaTime;
        }

        else if (yVelocity > 0 && !jumpAction.action.IsPressed())
        {
            yVelocity += gravity * lowJumpMultiplier * Time.deltaTime;
        }

        else
        {
            yVelocity += gravity * Time.deltaTime;
        }

        if (!canMove)
        {
            move = Vector3.zero;
        }

        Vector3 finalMove = move * playerSpeed + Vector3.up * yVelocity;
        controller.Move(finalMove * Time.deltaTime);

        //Debug.Log("Velocity:" + yVelocity);
    }

    IEnumerator EnableCanMove(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canMove = true;
    }
}
