using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    private VideoPlayer cutscenePlayer;
    public bool autoplay;

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
        if (autoplay)
        {
            if (other.CompareTag("Player"))
            {
                cutscenePlayer.Play();

                Time.timeScale = 0f;

                Debug.Log("Cutscene playing");
            }
        }

        else
        {
            if (other.CompareTag("Player") && Input.GetKey(KeyCode.E))
            {
                cutscenePlayer.Play();

                Time.timeScale = 0f;

                Debug.Log("Cutscene playing");
            }
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
