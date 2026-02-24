using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public int currentLevel = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(currentLevel + 1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
