using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class LedgeGrabbing : MonoBehaviour
{
    private int ledgeLayer;
    public Camera cam;
    private float playerHeight = 2f;
    private float playerRadius = 0.5f;

    public InputActionReference jumpAction;

    public CharacterController controller;

    void Start()
    {
        ledgeLayer = LayerMask.NameToLayer("Platforms");
    }

    // Update is called once per frame
    void Update()
    {
        Grab();
    }

    private void Grab()
    {
        if (controller.velocity.y < 0)
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out var firstHit, 1f, ledgeLayer))
            {
                Debug.Log("Ledge found");
                if (Physics.Raycast(firstHit.point + (cam.transform.forward * playerRadius) + (Vector3.up * 0.6f * playerHeight), Vector3.down, out var secondHit, playerHeight))
                {
                    Debug.Log("Landing point found");
                    StartCoroutine(LerpGrab(secondHit.point, 0.5f));
                }
            }
        }
    }

    IEnumerator LerpGrab(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
}
