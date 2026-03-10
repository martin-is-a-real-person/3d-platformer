using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    public float playerSpeed = 5.0f;
    public float jumpHeight = 1.5f;
    private float gravityValue = -12f;
    public float jumpCancelledMultiplier = 3.0f;
    private Vector3 move;

    public CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private bool hanging;
    private bool canMove = true;

    public float forwardDistance;

    private Transform cameraTransform;

    [SerializeField] private float coyoteTime = 0.5f;
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

            // Slight downward velocity to keep grounded stable
            if (playerVelocity.y < -2f)
                playerVelocity.y = -2f;
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
            gravityValue = -9.81f;
            hanging = false;
            StartCoroutine(EnableCanMove(0.25f));
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
        }

        else if (coyoteTimeCounter > 0f && jumpAction.action.WasPressedThisFrame())
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
            coyoteTimeCounter = 0f;
            SoundManager.PlaySound(SoundType.SHORTJUMP, 1);
        }

        if (playerVelocity.y > 0 && jumpAction.action.IsPressed() == false)
        {
            // Apply extra downward force (increased gravity)
            playerVelocity.y += gravityValue * jumpCancelledMultiplier * Time.deltaTime;
        }

        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;

        if (!canMove)
        {
            move = Vector3.zero;
        }

        // Move
        Vector3 finalMove = move * playerSpeed + Vector3.up * playerVelocity.y;
        controller.Move(finalMove * Time.deltaTime);
    }

    IEnumerator EnableCanMove(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canMove = true;
    }

    void LedgeGrab()
    {
        if (controller.velocity.y < 0 && !hanging)
        {
            RaycastHit downHit;
            Vector3 lineDownStart = transform.position + Vector3.up * -2f + transform.forward;
            Vector3 lineDownEnd = transform.position + Vector3.up * -1f + transform.forward;
            Physics.Linecast(lineDownStart, lineDownEnd, out downHit, LayerMask.GetMask("Platforms"));
            Debug.DrawLine(lineDownStart, lineDownEnd);

            if (downHit.collider != null)
            {
                RaycastHit forwardHit;
                Vector3 lineForwardStart = new Vector3(transform.position.x, downHit.point.y + 0.1f, transform.position.z);
                Vector3 lineForwardEnd = new Vector3(transform.position.x, downHit.point.y + 0.1f, transform.position.z) + transform.forward * forwardDistance;
                Physics.Linecast(lineForwardStart, lineForwardEnd, out forwardHit, LayerMask.GetMask("Platforms"));
                Debug.DrawLine(lineForwardStart, lineForwardEnd);

                if (forwardHit.collider != null)
                {
                    hanging = true;
                    canMove = false;

                    //climbing animation

                    Vector3 hangingPos = new Vector3(forwardHit.point.x, downHit.point.y, forwardHit.point.z);
                    Vector3 finalPos = new Vector3(forwardHit.point.x - 3, downHit.point.y - 3, forwardHit.point.z - 3);

                    transform.position = hangingPos;
                    transform.forward = -forwardHit.normal;

                    Vector3.MoveTowards(hangingPos, finalPos, 5);

                    SoundManager.PlaySound(SoundType.GRAB, 1);

                    hanging = false;
                    canMove = true;
                }
            }
        }
    }
}
