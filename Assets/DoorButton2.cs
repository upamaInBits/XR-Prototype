using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;   // <— add this

public class DoorButton2 : MonoBehaviour
{
    [Header("Unique door id (Door1, Door2, Door3, ...)")]
    public string doorId = "Door3";

    [Header("The visual parent (full door model)")]
    public GameObject doorRoot;

    [Header("Info scene to load (must be in Build Settings)")]
    public string infoSceneName = "InfoScene(2)";

    [Header("Animation (optional)")]
    public Animator doorAnimator;
    public float animationDelay = 2.0f;

    [Header("Open rotation when done")]
    public float openYAngle = -90f;   // tweak if it swings wrong way

    void Start()
    {
        bool alreadyDone =
            DoorProgress.Instance != null &&
            DoorProgress.Instance.IsDoorDone(doorId);

        if (alreadyDone)
        {
            // 1. Make the door stay visible & open
            if (doorRoot != null)
                doorRoot.transform.localRotation =
                    Quaternion.Euler(0f, openYAngle, 0f);

            // 2. Disable interaction
            var col = GetComponent<Collider>();
            if (col != null) col.enabled = false;

            var xr = GetComponent<XRBaseInteractable>();
            if (xr != null) xr.enabled = false;

            // 3. Disable this script too
            enabled = false;
            return;
        }

        // Not completed yet → normal door
        if (doorRoot != null)
            doorRoot.SetActive(true);
    }

    // Hook THIS to Door 3's click
    public void OnDoorPressed2()
    {
        // Safety: don’t open if already done
        if (DoorProgress.Instance != null &&
            DoorProgress.Instance.IsDoorDone(doorId))
            return;

        if (doorAnimator != null)
            doorAnimator.SetTrigger("Open");

        StartCoroutine(LoadAfterAnimation());
    }

    private IEnumerator LoadAfterAnimation()
    {
        SceneTracker.PreviousScene = SceneManager.GetActiveScene().name;

        yield return new WaitForSeconds(animationDelay);

        SceneManager.LoadScene(infoSceneName, LoadSceneMode.Single);
    }
} 