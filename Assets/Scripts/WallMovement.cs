using UnityEngine;

public class WallMovement : MonoBehaviour
{
    public float wallSpeed;
    private Rigidbody rb;
    public Vector3 movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(movement * wallSpeed * Time.deltaTime);
    }
}
