using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{

    private Vector3 lastCheckPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastCheckPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            GetComponent<CharacterController>().enabled = false;
            transform.position = lastCheckPoint;
            GetComponent<CharacterController>().enabled = true;
        }
    }
}
