using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;   

public class DoorButton1 : MonoBehaviour
{
    [Header("Unique door id (Door1, Door2, Door3, ...)")]
    public string doorId = "Door2";

    [Header("The visual parent (the full door model)")]
    public GameObject doorRoot;

    [Header("Info scene to load (must be in Build Settings)")]
    public string infoSceneName = "InfoScene(1)";

    [Header("Animation (optional)")]
    public Animator doorAnimator;
    public float animationDelay = 2.0f; 

    [Header("Open rotation when done")]
    public float openYAngle = -90f;   

    void Start()
    {
        bool alreadyDone =
            DoorProgress.Instance != null &&
            DoorProgress.Instance.IsDoorDone(doorId);

        if (alreadyDone)
        {
            // 1. Make sure door is visible + open
            if (doorRoot != null)
                doorRoot.transform.localRotation =
                    Quaternion.Euler(0f, openYAngle, 0f);

            // 2. Disable interaction (collider + XR interactable)
            var col = GetComponent<Collider>();
            if (col != null) col.enabled = false;

            var xr = GetComponent<XRBaseInteractable>();
            if (xr != null) xr.enabled = false;

            // 3. Disable this script
            enabled = false;
            return;
        }

        // Not completed yet → normal door
        if (doorRoot != null)
            doorRoot.SetActive(true);
    }

    // Hook THIS to Door 2's click
    public void OnDoorPressed1()
    {
        if (DoorProgress.Instance != null &&
            DoorProgress.Instance.IsDoorDone(doorId))
            return; // safety: ignore if already done

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
