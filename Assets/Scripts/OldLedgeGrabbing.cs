using UnityEngine;
using UnityEngine.InputSystem;

public class OldLedgeGrabbing : MonoBehaviour
{
    // Refrences
    public MovementScript ms;
    public Transform orientation;
    public Transform cam;
    public InputActionReference moveAction;

    // Ledge grabbing
    public float moveToLedgeSpeed;
    public float maxLedgeGrabDistance;

    public float minTimeOnLedge;
    private float timeOnLedge;

    public bool holding;
    public CharacterController cc;

    // Ledge detection
    public float ledgeDetectionLength;
    public float ledgeSphereCastRadius;
    public LayerMask whatIsLedge;

    private Transform lastLedge;
    private Transform currentLedge;

    private RaycastHit ledgeHit;

    // Ledge climbing
    public InputActionReference climbAction;
    public float ledgeClimbForwardForce;
    public float ledgeClimbUpwardForce;

    // Exiting
    public bool exitingLedge;
    public float exitLedgeTime;
    private float exitLedgeTimer;

    void Update()
    {
        LedgeDetection();
        SubStateMachine();
    }

    private void SubStateMachine()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        bool anyInputKeyPressed = move != Vector3.zero;

        // Holding onto ledge
        if (holding)
        {
            FreezeOnLedge();

            timeOnLedge += Time.deltaTime;

            if (timeOnLedge > minTimeOnLedge && anyInputKeyPressed)
            {
                ExitLedgeHold();
            }

            if (climbAction.action.WasPressedThisFrame())
            {
                LedgeClimb();
            }
        }

        // Exiting ledge
        else if (exitingLedge)
        {
            if(exitLedgeTimer > 0)
            {
                exitLedgeTimer -= Time.deltaTime;
            }

            else
            {
                exitingLedge = false;
            }
        }
    }

    private void LedgeDetection()
    {
        bool ledgeDetected = Physics.SphereCast(transform.position, ledgeSphereCastRadius, cam.forward, out ledgeHit, ledgeDetectionLength, whatIsLedge);

        if (!ledgeDetected)
        {
            return;
        }

        float distanceToLedge = Vector3.Distance(transform.position, ledgeHit.transform.position);

        if (ledgeHit.transform == lastLedge)
        {
            return;
        }

        if (distanceToLedge < maxLedgeGrabDistance && !holding)
        {
            EnterLedgeHold();
        }
    }

    private void LedgeClimb()
    {
        ExitLedgeHold();

        Invoke(nameof(DelayedJumpForce), 0.05f);
    }

    private void DelayedJumpForce()
    {
        Vector3 forceToAdd = cam.forward * ledgeClimbForwardForce + orientation.up * ledgeClimbUpwardForce;
        cc.Move(forceToAdd);
    }

    private void EnterLedgeHold()
    {
        holding = true;

        currentLedge = ledgeHit.transform;
        lastLedge = ledgeHit.transform;
    }

    private void FreezeOnLedge()
    {
        Vector3 directionToLedge = currentLedge.position - transform.position;
        float distanceToLedge = Vector3.Distance(transform.position, currentLedge.position);

        // Move player towards ledge
        if(distanceToLedge > 1f)
        {
            if(cc.velocity.magnitude < moveToLedgeSpeed)
            {
                cc.Move(directionToLedge.normalized * moveToLedgeSpeed * Time.deltaTime);
            }
        }

        // Hold onto ledge
        else
        {
            if (!holding)
            {
                holding = true;
            }
        }

        // Exiting if something goes wrong
        if(distanceToLedge > maxLedgeGrabDistance)
        {
            ExitLedgeHold();
        }
    }

    private void ExitLedgeHold()
    {
        exitingLedge = true;
        exitLedgeTimer = exitLedgeTime;

        holding = false;
        timeOnLedge = 0f;

        StopAllCoroutines();
        Invoke(nameof(ResetLastLedge), 1f);
    }

    private void ResetLastLedge()
    {
        lastLedge = null;
    }
}
