using UnityEngine;

public class DataUseAudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource introSource;     // "This game uses eye tracking, location & camera"
    public AudioSource eyeSource;       // eye explanation
    public AudioSource locationSource;  // location explanation
    public AudioSource cameraSource;    // camera explanation

    private AudioSource currentSource;

    void Start()
    {
        // Start with intro as the active source, but NOT playing yet
        SetCurrentSource(introSource, false);
    }

    public void SetCurrentSource(AudioSource newSource, bool autoPlay)
    {
        if (currentSource != null && currentSource != newSource)
        {
            currentSource.Stop();
        }

        currentSource = newSource;

        if (autoPlay && currentSource != null)
        {
            currentSource.time = 0f;
            currentSource.Play();
        }
    }

    public void TogglePlayPause()
    {
        if (currentSource == null) return;

        if (currentSource.isPlaying)
        {
            // currently playing → pause it
            currentSource.Pause();
        }
        else
        {
            // currently stopped or paused → PLAY from current time
            currentSource.Play();
        }
    }

    public void UseIntroAudio()    { SetCurrentSource(introSource, false); }
    public void UseEyeAudio()      { SetCurrentSource(eyeSource, true); }
    public void UseLocationAudio() { SetCurrentSource(locationSource, true); }
    public void UseCameraAudio()   { SetCurrentSource(cameraSource, true); }

    // ⬇⬇⬇ ADD THIS ⬇⬇⬇
    public AudioSource GetCurrentSource()
    {
        return currentSource;
    }
}
