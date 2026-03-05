using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class LedgeGrabbing : MonoBehaviour
{
    private int ledgeLayer;
    public Transform playerEyes;
    private float playerHeight = 2f;
    private float playerRadius = 0.5f;
    public Collider playerCollider;

    public InputActionReference jumpAction;

    public CharacterController controller;
    private Transform cameraTransform;

    void Start()
    {
        ledgeLayer = LayerMask.NameToLayer("Platforms");
        ledgeLayer = ~ledgeLayer;
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        playerEyes = cameraTransform;
        Grab();
    }

    private void Grab()
    {
        if (controller.velocity.y < 0)
        {
            if (Physics.Raycast(playerEyes.transform.position, playerEyes.transform.TransformDirection(Vector3.forward), out var firstHit, 1f, ledgeLayer))
            {
                Debug.Log("Ledge found");
                if (Physics.Raycast(firstHit.point + (playerEyes.transform.TransformDirection(Vector3.forward) * playerRadius) + (Vector3.up * 0.6f * playerHeight), Vector3.down, out var secondHit, playerHeight))
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
        playerCollider.enabled = false;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        playerCollider.enabled = true;
    }
}
