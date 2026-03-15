using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            CheckpointManager.Instance.SavedCheckpoint(other.transform.position);

            Debug.Log("Checkpoint Saved");
        }
    }
}
