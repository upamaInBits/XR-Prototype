using UnityEngine;
using UnityEngine.UI;

public class ParentScreenshotController : MonoBehaviour
{
    public Button saveButton;
    public Button playButton;
    public AudioSource shutterSound;   // <-- ADD THIS

    bool screenshotTaken = false;

    void Start()
    {
        if (playButton != null)
            playButton.interactable = false;
    }

    public void SaveParentPage()
    {
        // Save screenshot (fake or real)
        string fileName = System.DateTime.Now.ToString("'ParentSummary_'yyyyMMdd_HHmmss'.png'");
        ScreenCapture.CaptureScreenshot(fileName);

        // 🔊 Play shutter sound
        if (shutterSound != null)
            shutterSound.Play();

        // Enable Play button
        screenshotTaken = true;
        playButton.interactable = true;

        Debug.Log("Parent summary screenshot saved: " + fileName);
    }
}
