using UnityEngine;
using UnityEngine.SceneManagement;

public class NewClosePanel : MonoBehaviour
{
    [Header("Name of your hub scene as backup")]
    public string fallbackReturnScene = "working_version";

    [Header("Which door this InfoScene corresponds to")]
    public string doorId = "Door1";   // MUST match DoorButton.doorId in the hub

    // OK button → mark door done, then go back (unless ReadyScene is loading)
    public void CloseAndReturn()
    {
        // 1) Mark this door as completed
        if (DoorProgress.Instance != null)
        {
            Debug.Log($"[NewClosePanel] Marking door done: {doorId}");
            DoorProgress.Instance.MarkDoorDone(doorId);

            // If this was the last door, DoorProgress.MarkDoorDone()
            // will already load ReadyScene for you.
            if (DoorProgress.Instance.AreAllDoorsDone())
            {
                Debug.Log("[NewClosePanel] All doors done → ReadyScene is loading, not returning to hub.");
                return;
            }
        }
        else
        {
            Debug.LogWarning("[NewClosePanel] DoorProgress.Instance is NULL");
        }

        // 2) Go back to whichever scene we came from (hub)
        string target = string.IsNullOrEmpty(SceneTracker.PreviousScene)
            ? fallbackReturnScene
            : SceneTracker.PreviousScene;

        Debug.Log("[NewClosePanel] Returning to: " + target);
        SceneManager.LoadScene(target, LoadSceneMode.Single);
    }

    // Back button → DO NOT mark door done, just return
    public void BackWithoutCompleting()
    {
        Debug.Log("[NewClosePanel] BackWithoutCompleting called for " + doorId);

        string target = string.IsNullOrEmpty(SceneTracker.PreviousScene)
            ? fallbackReturnScene
            : SceneTracker.PreviousScene;

        SceneManager.LoadScene(target, LoadSceneMode.Single);
    }
}
