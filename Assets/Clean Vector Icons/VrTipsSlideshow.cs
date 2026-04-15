using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class VRTipsSlideshow : MonoBehaviour
{
    [Header("UI")]
    public Image displayImage;
    public Button leftButton;
    public Button rightButton;

    [Header("Slides")]
    public Sprite[] slides;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] slideAudio;

    [Header("Progress")]
    public Slider progressBar;
    public TextMeshProUGUI progressText;
    public Button okButton;

    [Header("Volume Icon")]
    public VolumeToggle volumeToggle;   // drag your Volume_button here

    private int index = 0;
    private bool[] seenSlides;

    // audio state
    private bool isPlaying = false;   // audio actively playing
    private bool isPaused  = false;   // audio paused mid-clip

    void Start()
    {
        if (slides != null && slides.Length > 0)
        {
            seenSlides = new bool[slides.Length];

            if (progressBar != null)
            {
                progressBar.minValue = 1f;
                progressBar.maxValue = slides.Length;
                progressBar.wholeNumbers = true;
            }

            if (okButton != null)
                okButton.gameObject.SetActive(false);

            ShowSlide(0);
        }
    }

    void ShowSlide(int i)
    {
        if (slides == null || slides.Length == 0) return;

        // RESET state on slide change
        isPlaying = false;
        isPaused  = false;
        SetNavButtonsInteractable(true);
        StopAllCoroutines();

        index = Mathf.Clamp(i, 0, slides.Length - 1);

        // show slide
        if (displayImage != null)
            displayImage.sprite = slides[index];

        // mark seen
        if (seenSlides != null)
            seenSlides[index] = true;

        UpdateUIForCurrentSlide();

        // reset icon to ON (audio will auto-play)
        if (volumeToggle != null)
            volumeToggle.ForceOn();

        AutoPlayCurrentAudio();
    }

    void AutoPlayCurrentAudio()
    {
        if (audioSource == null || slideAudio == null)
        {
            SetNavButtonsInteractable(true);
            return;
        }

        if (index < 0 || index >= slideAudio.Length)
        {
            SetNavButtonsInteractable(true);
            return;
        }

        AudioClip clip = slideAudio[index];
        if (clip == null)
        {
            SetNavButtonsInteractable(true);
            return;
        }

        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.time = 0f;
        audioSource.Play();

        isPlaying = true;
        isPaused  = false;
        SetNavButtonsInteractable(false);
        
        if (volumeToggle != null)
            volumeToggle.UpdateIcon();

        StopAllCoroutines();
        StartCoroutine(WaitForClipEnd());
    }

    IEnumerator WaitForClipEnd()
    {
        while (audioSource != null && audioSource.isPlaying)
            yield return null;

        // if audio truly finished (not paused)
        if (!isPaused)
        {
            isPlaying = false;
            isPaused  = false;
            SetNavButtonsInteractable(true);
        }

        if (volumeToggle != null)
            volumeToggle.UpdateIcon();
    }

    void UpdateUIForCurrentSlide()
    {
        int currentNumber = index + 1;

        if (progressBar != null)
            progressBar.value = currentNumber;

        if (progressText != null)
            progressText.text = currentNumber + "/" + slides.Length;

        if (okButton != null)
            okButton.gameObject.SetActive(AllSlidesSeen());
    }

    bool AllSlidesSeen()
    {
        if (seenSlides == null) return false;

        for (int i = 0; i < seenSlides.Length; i++)
            if (!seenSlides[i]) return false;

        return true;
    }

    public void NextSlide()
    {
        if (!CanChangeSlide()) return;

        int newIndex = index + 1;
        if (newIndex >= slides.Length) newIndex = 0;

        ShowSlide(newIndex);
    }

    public void PreviousSlide()
    {
        if (!CanChangeSlide()) return;

        int newIndex = index - 1;
        if (newIndex < 0) newIndex = slides.Length - 1;

        ShowSlide(newIndex);
    }

    bool CanChangeSlide()
    {
        // Only allow changing when audio is finished
        return !isPlaying && !isPaused;
    }

    // 🔊 Volume button calls this
    public void ToggleCurrentAudio()
    {
        if (audioSource == null || audioSource.clip == null) return;

        // CASE 1 — Playing → PAUSE
        if (isPlaying && audioSource.isPlaying)
        {
            audioSource.Pause();
            isPlaying = false;
            isPaused  = true;

            if (volumeToggle != null)
                volumeToggle.UpdateIcon();

            return;
        }

        // CASE 2 — Paused → RESUME
        if (isPaused)
        {
            audioSource.UnPause();
            isPlaying = true;
            isPaused  = false;

            if (volumeToggle != null)
                volumeToggle.UpdateIcon();

            StopAllCoroutines();
            StartCoroutine(WaitForClipEnd());
            return;
        }

        // CASE 3 — Finished → PLAY from start
        audioSource.time = 0f;
        audioSource.Play();
        isPlaying = true;
        isPaused  = false;

        SetNavButtonsInteractable(false);

        if (volumeToggle != null)
            volumeToggle.UpdateIcon();

        StopAllCoroutines();
        StartCoroutine(WaitForClipEnd());
    }

    void SetNavButtonsInteractable(bool value)
    {
        if (leftButton != null)  leftButton.interactable  = value;
        if (rightButton != null) rightButton.interactable = value;
    }

    // 🔍 VolumeToggle uses this to update icon
    public bool IsAudioActuallyPlaying()
    {
        return audioSource != null && audioSource.isPlaying;
    }
}
