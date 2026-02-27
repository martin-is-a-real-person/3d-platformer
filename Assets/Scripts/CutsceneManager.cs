using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    private VideoPlayer cutscenePlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cutscenePlayer = GetComponent<VideoPlayer>();

        cutscenePlayer.Prepare();

        cutscenePlayer.loopPointReached += OnCutsceneFinished;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            cutscenePlayer.Play();

            Time.timeScale = 0f;

            Debug.Log("Cutscene playing");
        }

    }

    void OnCutsceneFinished(VideoPlayer vp)
    {
        cutscenePlayer.Stop();

        Time.timeScale = 1f;

        cutscenePlayer.targetCamera = null;

        gameObject.SetActive(false);
    }
}
