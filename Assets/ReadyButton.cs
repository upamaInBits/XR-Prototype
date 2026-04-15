using UnityEngine;

public class ReadyButtonController : MonoBehaviour
{
    [Header("Scene Objects")]
    public GameObject dog;          // assign the puppy root
    public GameObject doorsRoot;    // parent of the 3 doors
    public AudioSource introAudio;  // optional: the intro voice
    public GameObject readyButton;  // the UI Button GameObject (not the component)

    private static bool alreadyPressed;

    void Start()
    {
        ApplyState();
    }

    // Hook this to the Button's OnClick() -> ReadyButtonController.OnReady
    public void OnReady()
    {
        alreadyPressed = true;

        if (introAudio && introAudio.isPlaying) introAudio.Stop();

        if (dog)        dog.SetActive(false);
        if (doorsRoot)  doorsRoot.SetActive(true);
        if (readyButton) readyButton.SetActive(false);
    }

    void ApplyState()
    {
        if (alreadyPressed)
        {
            if (dog)        dog.SetActive(false);
            if (doorsRoot)  doorsRoot.SetActive(true);
            if (readyButton) readyButton.SetActive(false);
        }
        else
        {
            if (dog)        dog.SetActive(true);
            if (doorsRoot)  doorsRoot.SetActive(false);
            if (readyButton) readyButton.SetActive(true);   // show until pressed
        }
    }
}
