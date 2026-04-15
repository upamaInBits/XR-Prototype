using UnityEngine;
using UnityEngine.UI;

public class VolumeToggle : MonoBehaviour
{
    public Image icon;
    public Sprite volumeOnSprite;     // 🔊
    public Sprite volumeOffSprite;    // 🔇

    public VRTipsSlideshow slideshow; // reference to slideshow

    void Start()
    {
        UpdateIcon();
    }

    public void UpdateIcon()
    {
        if (icon == null || slideshow == null) return;

        if (slideshow.IsAudioActuallyPlaying())
            icon.sprite = volumeOnSprite;
        else
            icon.sprite = volumeOffSprite;
    }

    public void OnVolumeButtonClicked()
    {
        // Called AFTER ToggleCurrentAudio()
        UpdateIcon();
    }

    public void ForceOn()
    {
        if (icon != null && volumeOnSprite != null)
            icon.sprite = volumeOnSprite;
    }
}
