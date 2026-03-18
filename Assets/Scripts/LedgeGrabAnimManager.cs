using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class LedgeGrabAnimManager : MonoBehaviour
{
    private VideoPlayer animationPlayer;

    private bool isReady = false;

    IEnumerator Start()
    {
        animationPlayer = GetComponent<VideoPlayer>();
        animationPlayer.loopPointReached += OnAnimationFinished;

        yield return PrepareVideo();
    }

    public void PlayLedgeGrabAnimation()
    {
        if (!isReady)
        {
            Debug.LogWarning("Video not ready yet!");
            return;
        }

        animationPlayer.time = 0;
        animationPlayer.Play();

        Debug.Log("Animation playing instantly");
    }

    private IEnumerator PrepareVideo()
    {
        isReady = false;

        animationPlayer.Prepare();

        while (!animationPlayer.isPrepared)
            yield return null;

        isReady = true;
    }

    void OnAnimationFinished(VideoPlayer vp)
    {
        animationPlayer.Stop();

        StartCoroutine(PrepareVideo());
    }
}
