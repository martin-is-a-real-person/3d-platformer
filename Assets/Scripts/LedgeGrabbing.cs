using UnityEngine;
using UnityEngine.InputSystem;

public class LedgeGrabbing : MonoBehaviour
{
    // References
    public MovementScript ms;
    public Transform orientation;
    public Transform cam;
    public InputActionReference moveAction;

    // Ledge detection
    public float ledgeDetectionLength;
    public float ledgeSphereCastRadius;
    public LayerMask whatIsLedge;

    private Transform lastLedge;
    private Transform currentLedge;

    private RaycastHit ledgeHit;

    // Ledge grabbing
    public float maxLedgeGrabDistance;
    public bool holding;

    // Update is called once per frame
    void Update()
    {
        LedgeDetect();
    }

    private void LedgeDetect()
    {
        bool ledgeDetected = Physics.SphereCast(transform.position, ledgeSphereCastRadius, cam.forward, out ledgeHit, ledgeDetectionLength, whatIsLedge);

        if (!ledgeDetected)
        {
            return;
        }

        if (ledgeHit.transform == lastLedge)
        {
            return;
        }

        float distanceToLedge = Vector3.Distance(transform.position, ledgeHit.transform.position);

        if (distanceToLedge < maxLedgeGrabDistance && !holding)
        {
            EnterLedgeHold();
        }
    }

    private void EnterLedgeHold()
    {
        holding = true;

        currentLedge = ledgeHit.transform;
        lastLedge = ledgeHit.transform;
    }
}
