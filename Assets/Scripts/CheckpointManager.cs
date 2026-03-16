using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    //So other scripts can refer to this one
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

    //If there is no checkpoint manager in the scene, use this one
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
        //Disable the ledge grab, else the two scripts fight on who should move the player (the ledge grab always wins)
        LedgeGrabbing.disableLedgeGrab = true;
        Debug.Log("Ledge grab disabled");
        
        //Stop currently in progress ledge grabs
        player.GetComponent<LedgeGrabbing>().StopAllCoroutines();

        //Get the character controller on the player
        CharacterController cc = player.GetComponent<CharacterController>();

        //Disable the character controller, move the player, and enable the cc again (to stop physic conflicts from happening)
        cc.enabled = false;
        player.position = savedPlayerPos;
        cc.enabled = true;

        //Put the wall in the same state as when the checkpoint was saved
        wall.RestoreState(savedWallPos, savedWallSpeed, savedWallMode);

        //The spikes cannot damage the player while respawning
        SpikeDamage.canKill = false;

        //Enable spike damage and ledge grab after spesified time
        Invoke(nameof(EnableSpikeDamage), 1f);

        Invoke(nameof(ReEnableLedgeGrab), 1f);

    }

    void EnableSpikeDamage()
    {
        SpikeDamage.canKill = true;
    }

    void ReEnableLedgeGrab()
    {
        LedgeGrabbing.disableLedgeGrab = false;
        Debug.Log("Ledge grab enabled");
    }
}
