using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseSelfOverlay : MonoBehaviour
{
    public string sceneName = "InfoScene1";
    public void Close() => SceneManager.UnloadSceneAsync(sceneName);
}

