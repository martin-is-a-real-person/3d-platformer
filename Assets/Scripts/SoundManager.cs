using UnityEngine;

public enum SoundType
{
    LONGJUMP,
    SHORTJUMP,
    LAND,
    GRAB,
    FOOTSTEP
}

[RequireComponent(typeof(AudioSource))]

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static SoundManager instance;
    private AudioSource audioSource;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }
}
