using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleBackToHub : MonoBehaviour
{
    public string fallbackReturnScene = "working_version";

    public void Back()
    {
        string target = string.IsNullOrEmpty(SceneTracker.PreviousScene)
            ? fallbackReturnScene
            : SceneTracker.PreviousScene;

        SceneManager.LoadScene(target, LoadSceneMode.Single);
    }
}
