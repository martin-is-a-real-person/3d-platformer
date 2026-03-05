using UnityEngine;

public class WallMovementController : MonoBehaviour
{
    public Rigidbody rb;
    
    public float baseSpeed = 2f;

    //How much the wall accelerates
    public float accelerationValue = 1f;

    //So it moves in the positive z direction 
    public Vector3 moveDirection = Vector3.forward;

    //By default the wall does not move
    private WallMovementMode currentmode = WallMovementMode.Paused;

    private float currentSpeed = 0f;

    //To handle switching between the modes
    private void FixedUpdate()
    {
        switch (currentmode)
        {
            case WallMovementMode.IncreasingSpeed:
                HandleIncreasingSpeed();
                break;

            case WallMovementMode.ConstantSpeed:
                HandleConstantSpeed();
                break;

            case WallMovementMode.Paused:
                rb.linearVelocity = Vector3.zero;
                break;
        }
    }

    //What the increasing speed mode does, and the limits to how fast and slow it can move
    void HandleIncreasingSpeed()
    {
        currentSpeed += accelerationValue * Time.fixedDeltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, baseSpeed * 3f);

        Vector3 velocity = moveDirection.normalized * currentSpeed;
        rb.linearVelocity = velocity;
    }


    //What the constant speed mode does
    void HandleConstantSpeed()
    {
        currentSpeed = baseSpeed;

        Vector3 velocity = moveDirection.normalized * currentSpeed;
        rb.linearVelocity = velocity;
    }


    //Method so the trigger script can change the mode, and controls the paused mode
    public void SetMovementMode(WallMovementMode mode)
    {
        currentmode = mode;

        if (mode == WallMovementMode.Paused)
            rb.linearVelocity = Vector3.zero;
    }
}
