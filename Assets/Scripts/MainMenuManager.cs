using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;

    public GameObject mainMenuObject;

    [SerializeField] private string nameOfScene1;
    [SerializeField] private string nameOfScene2;
    [SerializeField] private string nameOfScene3;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void Level1Start()
    {
        SceneManager.LoadScene(nameOfScene1);
    }

    public void Level2Start()
    {
        SceneManager.LoadScene(nameOfScene2);
    }

    public void Level3Start()
    {
        SceneManager.LoadScene(nameOfScene3);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
