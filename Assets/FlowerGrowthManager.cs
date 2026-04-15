using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlowerGrowthManager : MonoBehaviour
{
    [Header("References")]
    public FlowerSpawner flowerSpawner;      // script that spawned the flower
    public Button waterButton;               // Grow button
    public Button shrinkButton;              // Shrink button
    public Button saveButton;                // Save flower button
    public ParticleSystem waterParticles;    // water effect
    public ParticleSystem shrinkPoof;        // shrink effect
    public Slider growthSlider;              // progress bar on the board
    public MessageDisplay messageDisplay;    // text on the white board
    public TextMeshProUGUI timerText;        // timer label

    [Header("Slider Colors (optional)")]
    public Image sliderFillImage;            // drag the Fill object here
    public Color goodColor = Color.green;    // 0–5
    public Color overColor = Color.red;      // >5

    [Header("Growth Settings")]
    public float minScale   = 0.9f;
    public float maxScale   = 10f;
    public float growStep   = 0.2f;
    public float shrinkStep = 0.2f;

    [Header("Target Height")]
    public float targetScale = 5f;    // “perfect” height
    public float targetRange = 0.2f;  // how close counts as perfect

    [Header("Score UI")]
    public Button seeScoreButton;      // button that appears after saving
    public GameObject scorePanel;      // big popup panel
    public TextMeshProUGUI scoreText;  // text on the panel
    public Button exitAppButton;       // Exit app button (hook to OnExitAppClicked)

    // internal state
    bool isFinished   = false;
    bool timerRunning = false;
    float elapsedTime = 0f;

    // Convenience shortcut to the current flower transform
    Transform FlowerTransform
    {
        get
        {
            if (flowerSpawner == null || flowerSpawner.currentFlower == null)
                return null;
            return flowerSpawner.currentFlower.transform;
        }
    }

    // Are we close enough to the target height?
    bool IsAtTargetHeight()
    {
        Transform f = FlowerTransform;
        if (f == null) return false;

        float size = f.localScale.x;
        return Mathf.Abs(size - targetScale) <= targetRange;
    }

    // ---------------- START ----------------

    void Start()
    {
        // Slider: show real scale (0.9 → 10)
        if (growthSlider != null)
        {
            growthSlider.minValue = minScale;
            growthSlider.maxValue = maxScale;
        }

        // Save button: visible (you can decide interactable in Inspector)
        if (saveButton != null)
        {
            saveButton.gameObject.SetActive(true);
        }

        // See Score button: visible, but NOT clickable at start
        if (seeScoreButton != null)
        {
            seeScoreButton.gameObject.SetActive(true);   // visible from the beginning
            seeScoreButton.interactable = false;         // disabled until Save
        }

        // Score panel hidden at start
        if (scorePanel != null)
            scorePanel.SetActive(false);

        // Timer label
        if (timerText != null)
            timerText.text = "Time: 0.0s";

        RefreshSliderAndColor();
    }

    // ---------------- UPDATE ----------------

    void Update()
    {
        if (!timerRunning) return;

        elapsedTime += Time.deltaTime;

        if (timerText != null)
            timerText.text = $"Time: {elapsedTime:0.0}s";
    }

    // ---------------- SLIDER + COLOR ----------------

    void RefreshSliderAndColor()
    {
        float size = minScale;

        Transform f = FlowerTransform;
        if (f != null)
            size = Mathf.Clamp(f.localScale.x, minScale, maxScale);

        // 1) Slider position
        if (growthSlider != null)
            growthSlider.value = size;

        // 2) Bar color: 0–5 green, >5 red (optional)
        if (sliderFillImage != null)
        {
            if (size <= targetScale)
                sliderFillImage.color = goodColor;
            else
                sliderFillImage.color = overColor;
        }
    }

    // ---------------- TIMER CONTROL ----------------

    public void StartTimerIfNeeded()
    {
        if (isFinished) return;

        if (!timerRunning)
        {
            timerRunning = true;
            elapsedTime  = 0f;

            if (timerText != null)
                timerText.text = "Time: 0.0s";

            Debug.Log("[FlowerGrowth] Timer started.");
        }
    }

    // Enable/disable Save button based on height (currently no-op on purpose)
    public void UpdateSaveButtonState()
    {
        if (saveButton == null) return;
        // If you ever want logic again, add it here.
        // saveButton.interactable = !isFinished && IsAtTargetHeight();
    }

    // ---------------- GROW (Water) ----------------

    public void OnWaterButtonClicked()
    {
        if (isFinished) return;
        StartTimerIfNeeded();

        Transform f = FlowerTransform;
        if (f == null)
        {
            if (messageDisplay != null)
                messageDisplay.ShowTemporary("Oops! We need a flower first!");
            return;
        }

        float current = f.localScale.x;

        if (current >= maxScale)
        {
            if (waterParticles != null) waterParticles.Stop();
            if (waterButton != null)    waterButton.interactable = false;
            Debug.Log("[FlowerGrowth] Already at max size, ignoring.");
            return;
        }

        if (waterParticles != null)
            waterParticles.Play();

        float raw     = current + growStep;
        float clamped = Mathf.Clamp(raw, minScale, maxScale);

        // Snap exactly to target the first time we cross it
        if (current < targetScale && raw >= targetScale)
            clamped = targetScale;

        f.localScale = Vector3.one * clamped;

        Debug.Log("[FlowerGrowth] Grew flower to " + clamped);

        RefreshSliderAndColor();
        UpdateSaveButtonState();

        if (clamped >= maxScale)
        {
            if (waterParticles != null) waterParticles.Stop();
            if (waterButton != null)    waterButton.interactable = false;
        }
    }

    // ---------------- SHRINK ----------------

    public void OnShrinkButtonClicked()
    {
        if (isFinished)
            return;

        StartTimerIfNeeded();

        Transform f = FlowerTransform;
        if (f == null)
        {
            if (messageDisplay != null)
                messageDisplay.ShowTemporary("Grow a flower in the pot first!");
            return;
        }

        float current = f.localScale.x;

        if (current <= minScale + 0.01f)
        {
            if (messageDisplay != null)
                messageDisplay.ShowTemporary("This is the smallest it can be.");
            return;
        }

        Vector3 s = f.localScale;
        s -= Vector3.one * shrinkStep;

        float clamped = Mathf.Clamp(s.x, minScale, maxScale);
        f.localScale  = Vector3.one * clamped;

        // keep it upright
        f.rotation      = Quaternion.identity;
        f.localRotation = Quaternion.identity;

        Debug.Log("[FlowerGrowth] Shrunk flower to " + clamped);

        if (waterButton != null && clamped < maxScale)
            waterButton.interactable = true;

        RefreshSliderAndColor();
        UpdateSaveButtonState();

        // FX
        if (shrinkPoof != null)
        {
            shrinkPoof.transform.position = f.position + Vector3.up * 0.05f;
            shrinkPoof.Play();
        }

        var wiggle = f.GetComponent<ShrinkWiggle>();
        if (wiggle != null)
            wiggle.Wiggle();

        var audio = f.GetComponent<AudioSource>();
        if (audio != null)
            audio.Play();
    }

    // ---------------- SAVE FLOWER ----------------

    public void OnSaveButtonClicked()
    {
        // Prevent multiple saves
        if (isFinished)
            return;

        if (!IsAtTargetHeight())
        {
            if (messageDisplay != null)
                messageDisplay.ShowTemporary("Try to match the green bar before saving!");
            return;
        }

        isFinished   = true;
        timerRunning = false;

        if (waterButton != null)  waterButton.interactable = false;
        if (shrinkButton != null) shrinkButton.interactable = false;
        if (saveButton != null)   saveButton.interactable = false;

        float finalTime = elapsedTime;

        if (timerText != null)
            timerText.text = $"Time: {finalTime:0.0}s";

        // Prepare score text for the panel
        if (scoreText != null)
            scoreText.text = $"YAY! You grew your flower in {finalTime:0.0}s!";

        // Enable the See Score button (it was visible but disabled)
        if (seeScoreButton != null)
        {
            seeScoreButton.gameObject.SetActive(true); // already true, but safe
            seeScoreButton.interactable = true;        // NOW clickable
        }
    }

    // ---------------- SCORE PANEL + EXIT ----------------

    // Hook this to SeeScoreButton's OnClick
    public void OnSeeScoreClicked()
    {
        if (scorePanel != null)
            scorePanel.SetActive(true);
    }

    // Hook this to Exit button's OnClick on the score panel
    public void OnExitAppClicked()
    {
        Debug.Log("[FlowerGrowth] Exit app requested.");
        Application.Quit();
    }
}
