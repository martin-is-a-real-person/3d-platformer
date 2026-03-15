using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public static DeathMenu Instance;

    public GameObject deathUI;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowDeathMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        deathUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartCheckpoint()
    {
        deathUI.SetActive(false);

        CheckpointManager.Instance.Respawn();
        Time.timeScale = 1.0f;
    }


    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
