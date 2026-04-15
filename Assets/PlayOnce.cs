using UnityEngine;

public class PlayOnce : MonoBehaviour
{
    private static bool alreadyPlayed;

    [Header("The dog / intro character root object")]
    public GameObject dogRoot;   // <- assign the dog parent here

    [Header("Audio that should only play once")]
    public AudioSource source;

    void Start()
    {
        // If we've already played the intro,
        // hide the dog and stop the audio
        if (alreadyPlayed)
        {
            if (source) source.enabled = false;
            if (dogRoot) dogRoot.SetActive(false);   // <-- THIS stops the flash
            return;
        }

        // First time → play the audio and remember it
        if (source) source.Play();
        alreadyPlayed = true;
    }
}
