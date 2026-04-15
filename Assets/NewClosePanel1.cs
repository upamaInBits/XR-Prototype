using UnityEngine;
using UnityEngine.SceneManagement;

public class NewClosePanel1 : MonoBehaviour
{
    [Header("Name of your hub scene as backup")]
    public string fallbackReturnScene = "working_version";

    [Header("Which door this InfoScene corresponds to")]
    public string doorId = "Door2";

    public void CloseAndReturn1()
    {
        // Count this door as completed
        if (DoorProgress.Instance != null)
            DoorProgress.Instance.MarkDoorDone(doorId);
        else
            Debug.LogWarning("[NewClosePanel1] DoorProgress.Instance is NULL — ensure DoorProgress is in the hub scene.");

        //If all doors are completed, allow DoorProgress to switch to ReadyScene
        if (DoorProgress.Instance != null && DoorProgress.Instance.AreAllDoorsDone())
            return;

        // Otherwise, return to the hub scene we came from
        string target = string.IsNullOrEmpty(SceneTracker.PreviousScene)
            ? fallbackReturnScene
            : SceneTracker.PreviousScene;

        SceneManager.LoadScene(target, LoadSceneMode.Single);
    }
    public void BackWithoutCompleting1()
{
    // DO NOT mark door done here.

    string target = string.IsNullOrEmpty(SceneTracker.PreviousScene)
        ? fallbackReturnScene
        : SceneTracker.PreviousScene;

    SceneManager.LoadScene(target, LoadSceneMode.Single);
}

}

