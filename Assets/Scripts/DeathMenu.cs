using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public static DeathMenu Instance;

    public GameObject deathUI;

    //If there is no death menu in the scene, use this one
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    //For making the death menu appear on death, reference this in other scripts
    public void ShowDeathMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        deathUI.SetActive(true);
        Time.timeScale = 0f;
    }

    //Method for restarting from checkpoint after death
    public void RestartCheckpoint()
    {
        deathUI.SetActive(false);

        Debug.Log("Checkpoint button pressed");

        //Use the respawn method from checkpoint manager
        CheckpointManager.Instance.Respawn();
        Time.timeScale = 1.0f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //Method for restarting from the start
    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
