using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorVanish : MonoBehaviour
{
    public string doorId = "Door1";      // give each door a unique id: Door1, Door2, Door3...
    public GameObject doorRoot;          // drag the big parent you want to hide (the door mesh parent)
    public string infoSceneName = "InfoScene";  // the name of the info scene to load

    void Start()
    {
        // If this door was clicked earlier, hide it immediately when we come back
        if (GameState.Instance && GameState.Instance.IsDoorHidden(doorId))
        {
            if (doorRoot) doorRoot.SetActive(false);
            enabled = false; // disable this script so it’s “done”
        }
    }

    // Hook THIS to the door’s click (On Click/On Activate)
    public void OnDoorPressed()
    {
        // mark and hide the door
        if (GameState.Instance) GameState.Instance.HideDoor(doorId);
        if (doorRoot) doorRoot.SetActive(false);

        // remember the current scene name before we leave
        SceneTracker.PreviousScene = SceneManager.GetActiveScene().name;

        // load the info scene as a full standalone scene
        SceneManager.LoadScene(infoSceneName, LoadSceneMode.Single);
    }
}
