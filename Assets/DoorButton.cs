using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorButton : MonoBehaviour
{
    [Header("Unique door id (Door1, Door2, Door3, ...)")]
    public string doorId = "Door1";

    [Header("The visual parent (the full door model)")]
    public GameObject doorRoot;

    [Header("Info scene to load (must be in Build Settings)")]
    public string infoSceneName = "InfoScene";

    [Header("Animation")]
    public Animator doorAnimator;
    public float animationDelay = 2.0f;

    [Header("Stuff to disable when door is done")]
    public Collider doorCollider;          // assign your BoxCollider here
    public Behaviour doorInteractable;     // assign XR Simple Interactable OR your click script here

void Start()
{
    bool alreadyDone =
        DoorProgress.Instance != null &&
        DoorProgress.Instance.IsDoorDone(doorId);

    if (alreadyDone)
    {
        // 1. Make sure door is visible
        if (doorRoot != null)
        {
            // 👉 Set the rotation to an "open" angle.
            // Try -90, 90, or 120 until it looks right.
            doorRoot.transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
        }

        // 2. Make sure it’s NOT clickable anymore
        var col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        var xr = GetComponent<UnityEngine.XR.Interaction.Toolkit.XRBaseInteractable>();
        if (xr != null) xr.enabled = false;

        // 3. Disable this script too
        enabled = false;

        return;
    }

    // Not completed yet → normal door
    if (doorRoot != null)
        doorRoot.SetActive(true);
}


    // ------ DOOR PRESS ------
    public void OnDoorPressed()
    {
        // Extra safety: if somehow clicked after completion, ignore
        if (DoorProgress.Instance != null &&
            DoorProgress.Instance.IsDoorDone(doorId))
        {
            Debug.Log($"[DoorButton] {doorId} was already done, ignoring press.");
            return;
        }

        // Play animation
        if (doorAnimator != null)
            doorAnimator.SetTrigger("Open");

        // Load scene after animation
        StartCoroutine(LoadAfterAnimation());
    }

    private IEnumerator LoadAfterAnimation()
    {
        // Remember hub scene
        SceneTracker.PreviousScene = SceneManager.GetActiveScene().name;

        // small delay for door-opening animation
        yield return new WaitForSeconds(animationDelay);

        // Go to info scene
        SceneManager.LoadScene(infoSceneName, LoadSceneMode.Single);
    }
} 