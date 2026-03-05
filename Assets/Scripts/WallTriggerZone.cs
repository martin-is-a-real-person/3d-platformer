using UnityEngine;

public class WallTriggerZone : MonoBehaviour
{
    public WallMovementController wallController;

    //When the player stays in the trigger use the pause mode
    public WallMovementMode modeWhenPlayerStays = WallMovementMode.Paused;


    //When the player exits the trigger use the constant speed mode, or whichever selected in inspector
    public WallMovementMode modeWhenPlayerExits = WallMovementMode.ConstantSpeed;

    //When the player stays in the trigger, use the when player stays mode
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        wallController.SetMovementMode(modeWhenPlayerStays);

    }

    //When the player exits the trigger, use the when player exits mode
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        wallController.SetMovementMode(modeWhenPlayerExits);
    }
}
