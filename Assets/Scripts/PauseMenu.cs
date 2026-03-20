using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;

    public GameObject pauseUI;

    private bool pauseMenuActive = false;

    [SerializeField] private string sceneToLoad;

    //If there is no pause menu in the scene, use this one
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && pauseMenuActive == false)
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            pauseMenuActive = true;
        }

        else if (Input.GetKeyDown(KeyCode.Tab) && pauseMenuActive == true)
        {
            pauseUI.SetActive(false);
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            pauseMenuActive = false;

            Debug.Log("Pause menu removed");
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

}
