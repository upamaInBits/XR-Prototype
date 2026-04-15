using UnityEngine;
using UnityEngine.SceneManagement;

public class NewClosePanel2 : MonoBehaviour
{
    [Header("Name of your hub scene as backup")]
    public string fallbackReturnScene = "working_version";

    [Header("Which door this InfoScene corresponds to")]
    public string doorId = "Door3";

    // OK BUTTON → marks door done + returns
    public void CloseAndReturn2()
    {
        // Mark this door as completed.
        if (DoorProgress.Instance != null)
            DoorProgress.Instance.MarkDoorDone(doorId);
        else
            Debug.LogWarning("[NewClosePanel2] DoorProgress.Instance is NULL — ensure DoorProgress is in the hub scene.");

        // If all doors completed → DoorProgress will auto-load ReadyScene
        if (DoorProgress.Instance != null && DoorProgress.Instance.AreAllDoorsDone())
            return;

        // Otherwise return to hub
        string target = string.IsNullOrEmpty(SceneTracker.PreviousScene)
            ? fallbackReturnScene
            : SceneTracker.PreviousScene;

        SceneManager.LoadScene(target, LoadSceneMode.Single);
    }

    // BACK BUTTON → does NOT mark door done
    public void BackWithoutCompleting2()
    {
        // DO NOT call MarkDoorDone here!

        string target = string.IsNullOrEmpty(SceneTracker.PreviousScene)
            ? fallbackReturnScene
            : SceneTracker.PreviousScene;

        SceneManager.LoadScene(target, LoadSceneMode.Single);
    }
}
