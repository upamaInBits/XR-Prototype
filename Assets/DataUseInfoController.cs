using UnityEngine;
using UnityEngine.UI;

public class DataUseInfoController : MonoBehaviour
{
    [Header("Buttons")]
    public Button eyeButton;
    public Button locationButton;
    public Button cameraButton;
    public Button okButton;       // center OK button
    public Button replayButton;   // "Replay / Watch again" button

    [Header("Texts")]
    public GameObject eyeText;
    public GameObject locationText;
    public GameObject cameraText;

    private enum InfoType { None, Eye, Location, Camera }
    private InfoType active = InfoType.None;

    private bool eyeSeen = false;
    private bool locationSeen = false;
    private bool cameraSeen = false;

    public Color defaultDisabledColor = new Color(0.85f, 0.85f, 0.85f, 0.45f);
    public Color completedGreen      = new Color(0.55f, 0.90f, 0.55f, 1f);

    void Start()
    {
        SetAllTexts(false);

        SetIconInteractable(InfoType.Eye, true);
        SetIconInteractable(InfoType.Location, true);
        SetIconInteractable(InfoType.Camera, true);

        SetIconAlpha(eyeButton, 1f);
        SetIconAlpha(locationButton, 1f);
        SetIconAlpha(cameraButton, 1f);

        var cEye = eyeButton.colors;
        cEye.disabledColor = defaultDisabledColor;
        eyeButton.colors = cEye;

        var cLoc = locationButton.colors;
        cLoc.disabledColor = defaultDisabledColor;
        locationButton.colors = cLoc;

        var cCam = cameraButton.colors;
        cCam.disabledColor = defaultDisabledColor;
        cameraButton.colors = cCam;

        if (okButton != null)
            okButton.gameObject.SetActive(false);

        if (replayButton != null)
            replayButton.gameObject.SetActive(false);
    }

    public void OnEyeClicked()      { HandleIconClick(InfoType.Eye); }
    public void OnLocationClicked() { HandleIconClick(InfoType.Location); }
    public void OnCameraClicked()   { HandleIconClick(InfoType.Camera); }

    public void OnReplayClicked()
    {
        eyeSeen = false;
        locationSeen = false;
        cameraSeen = false;

        var cEye = eyeButton.colors;
        cEye.disabledColor = defaultDisabledColor;
        eyeButton.colors = cEye;

        var cLoc = locationButton.colors;
        cLoc.disabledColor = defaultDisabledColor;
        locationButton.colors = cLoc;

        var cCam = cameraButton.colors;
        cCam.disabledColor = defaultDisabledColor;
        cameraButton.colors = cCam;

        SetAllTexts(false);
        active = InfoType.None;

        SetIconInteractable(InfoType.Eye, true);
        SetIconInteractable(InfoType.Location, true);
        SetIconInteractable(InfoType.Camera, true);

        SetIconAlpha(eyeButton, 1f);
        SetIconAlpha(locationButton, 1f);
        SetIconAlpha(cameraButton, 1f);

        if (okButton != null)
        {
            okButton.gameObject.SetActive(false);
            okButton.interactable = false;
        }

        if (replayButton != null)
            replayButton.gameObject.SetActive(false);
    }

    private void HandleIconClick(InfoType type)
    {
        // Close if same icon is clicked again
        if (active == type)
        {
            switch (type)
            {
                case InfoType.Eye:
                    eyeSeen = true;
                    var cEye = eyeButton.colors;
                    cEye.disabledColor = completedGreen;
                    eyeButton.colors = cEye;
                    break;
                case InfoType.Location:
                    locationSeen = true;
                    var cLoc = locationButton.colors;
                    cLoc.disabledColor = completedGreen;
                    locationButton.colors = cLoc;
                    break;
                case InfoType.Camera:
                    cameraSeen = true;
                    var cCam = cameraButton.colors;
                    cCam.disabledColor = completedGreen;
                    cameraButton.colors = cCam;
                    break;
            }

            SetAllTexts(false);
            active = InfoType.None;

            SetIconInteractable(type, false);

            SetIconAlpha(eyeButton, 1f);
            SetIconAlpha(locationButton, 1f);
            SetIconAlpha(cameraButton, 1f);

            if (!eyeSeen)      SetIconInteractable(InfoType.Eye, true);
            if (!locationSeen) SetIconInteractable(InfoType.Location, true);
            if (!cameraSeen)   SetIconInteractable(InfoType.Camera, true);

            CheckCompletion();
            return;
        }

        // Open new icon
        active = type;

        eyeText.SetActive(type == InfoType.Eye);
        locationText.SetActive(type == InfoType.Location);
        cameraText.SetActive(type == InfoType.Camera);

        SetIconInteractable(InfoType.Eye, false);
        SetIconInteractable(InfoType.Location, false);
        SetIconInteractable(InfoType.Camera, false);
        SetIconInteractable(type, true);

        SetIconAlpha(eyeButton,      eyeSeen      ? 1f : 0.25f);
        SetIconAlpha(locationButton, locationSeen ? 1f : 0.25f);
        SetIconAlpha(cameraButton,   cameraSeen   ? 1f : 0.25f);

        switch (type)
        {
            case InfoType.Eye:      SetIconAlpha(eyeButton, 1f); break;
            case InfoType.Location: SetIconAlpha(locationButton, 1f); break;
            case InfoType.Camera:   SetIconAlpha(cameraButton, 1f); break;
        }
    }

    private void CheckCompletion()
    {
        if (eyeSeen && locationSeen && cameraSeen)
        {
            SetIconInteractable(InfoType.Eye, false);
            SetIconInteractable(InfoType.Location, false);
            SetIconInteractable(InfoType.Camera, false);

            if (okButton != null)
            {
                okButton.gameObject.SetActive(true);
                okButton.interactable = true;
            }

            if (replayButton != null)
                replayButton.gameObject.SetActive(true);
        }
    }

    private void SetIconInteractable(InfoType type, bool value)
    {
        switch (type)
        {
            case InfoType.Eye:
                if (eyeButton != null) eyeButton.interactable = value;
                break;
            case InfoType.Location:
                if (locationButton != null) locationButton.interactable = value;
                break;
            case InfoType.Camera:
                if (cameraButton != null) cameraButton.interactable = value;
                break;
        }
    }

    private void SetAllTexts(bool value)
    {
        if (eyeText != null)      eyeText.SetActive(value);
        if (locationText != null) locationText.SetActive(value);
        if (cameraText != null)   cameraText.SetActive(value);
    }

    private void SetIconAlpha(Button button, float alpha)
    {
        if (button == null) return;

        var img = button.GetComponent<Image>();
        if (img == null) return;

        Color c = img.color;
        c.a = alpha;
        img.color = c;
    }
    public AudioSource GetActiveAudio()
{
    if (eyeText.activeSelf)      return eyeText.GetComponent<AudioSource>();
    if (locationText.activeSelf) return locationText.GetComponent<AudioSource>();
    if (cameraText.activeSelf)   return cameraText.GetComponent<AudioSource>();

    // fallback → top general narration
    return GetComponent<AudioSource>();
}

}
