using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    //The player variable
    public Transform player;

    //The wall variable
    public WallMovementController wall;


    //The saved info variables
    private Vector3 savedPlayerPos;
    private Vector3 savedWallPos;
    private float savedWallSpeed;
    private WallMovementMode savedWallMode;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    //Get the info on game start, so if there are no checkpoints yet, the player respawns at start
    void Start()
    {
        savedPlayerPos = player.position;
        savedWallPos = wall.GetWallPosition();
        savedWallSpeed = wall.GetCurrentSpeed();
        savedWallMode = wall.GetCurrentMode();
    }


    //The save function, which uses the WallMovementController methods of saving info
    public void SavedCheckpoint(Vector3 playerpos)
    {
        savedPlayerPos = playerpos;

        savedWallPos = wall.GetWallPosition();
        savedWallSpeed = wall.GetCurrentSpeed();
        savedWallMode = wall.GetCurrentMode();
    }

    //What should happen on respawn
    public void Respawn()
    {
        CharacterController cc = player.GetComponent<CharacterController>();

        if (cc != null)
        {
            cc.enabled = false;
            Vector3 safePos = savedPlayerPos + Vector3.up * 1.5f;
            player.position = safePos;


            cc.enabled = true;
        }

        else
        {
            player.position = savedPlayerPos;
        }

            wall.RestoreState(savedWallPos, savedWallSpeed, savedWallMode);

        Debug.Log("Respawning player at: " + savedPlayerPos);
    }

}
