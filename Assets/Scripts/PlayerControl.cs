using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance;

    [SerializeField] private LedgeGrabbing ledgeGrabScript;
    [SerializeField] private MovementScript movementScript;

    [SerializeField] private GameObject playerObject;

    void Awake()
    {
        instance = this;
    }
    
    void Activate()
    {
        ledgeGrabScript.enabled = true;
        movementScript.enabled = true;
        playerObject.SetActive(true);
    }

    void Deactivate()
    {
        ledgeGrabScript.enabled = false;
        movementScript.enabled = false;
        playerObject.SetActive(false);
    }
}
