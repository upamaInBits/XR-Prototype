using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOpenLoadScene : MonoBehaviour
{
    [Header("Optional: hide this when door is pressed")]
    public GameObject toHide;

    [Header("Scene to open (must be in Build Settings)")]
    public string sceneToLoad;

    public bool loadAdditive = true;   // keeps current scene alive

    // Hook this to XR Simple Interactable → Select Entered
    public void Open()
    {
        if (toHide) toHide.SetActive(false);
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadSceneAsync(
                sceneToLoad,
                loadAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single
            );
        }
    }
}

