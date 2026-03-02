using UnityEngine;

public class WallMovement : MonoBehaviour
{
    public float wallSpeed;
    private Rigidbody rb;
    public Vector3 movement;
    public CharacterController controller;
    private bool hasMoved = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hasMoved)
        {
            if (controller.velocity.z != 0)
            {
                hasMoved = true;
            }
        }

        if (hasMoved)
        {
            rb.AddForce(movement * wallSpeed * Time.deltaTime);
        }
    }
}
