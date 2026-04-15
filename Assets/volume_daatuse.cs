using UnityEngine;
using UnityEngine.UI;

public class VolumeToggle_DataUse : MonoBehaviour
{
    public Image icon;
    public Sprite volumeOnSprite;
    public Sprite volumeOffSprite;

    public DataUseAudioManager audioManager; // reference to your audio manager

    void Start()
    {
        UpdateIcon();
    }

    // Call this from the button OnClick
    public void OnVolumeClicked()
    {
        if (audioManager != null)
        {
            audioManager.TogglePlayPause();
        }

        UpdateIcon();
    }

    public void UpdateIcon()
    {
        if (icon == null || audioManager == null)
            return;

        AudioSource activeAudio = audioManager.GetCurrentSource();

        if (activeAudio == null)
        {
            icon.sprite = volumeOffSprite;
            return;
        }

        icon.sprite = activeAudio.isPlaying ? volumeOnSprite : volumeOffSprite;
    }

    public void ForceOn()
    {
        if (icon != null)
            icon.sprite = volumeOnSprite;
    }
}
