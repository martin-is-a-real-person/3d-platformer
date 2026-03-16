using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    //Checkpoint saved at the players position when entering the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            CheckpointManager.Instance.SavedCheckpoint(other.transform.position);
        }
    }
}
