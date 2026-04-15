using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door2info : MonoBehaviour
{
    public string sceneName = "InfoScene2";
    public float hideDelay = 0.05f;
    public ParticleSystem burst;
    public GameObject doorVisualRoot;   // assign the parent that contains ALL renderers/colliders for the door

    public void Go() => StartCoroutine(PlayThenLoad());

    IEnumerator PlayThenLoad()
    {
        if (burst) burst.Play();

        // Hide the visuals after a tiny delay
        if (hideDelay > 0) yield return new WaitForSeconds(hideDelay);

        var toHide = doorVisualRoot ? doorVisualRoot : gameObject; // fallback to self
        toHide.SetActive(false);

       

        // Load overlay additively
        var op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!op.isDone) yield return null;

        var s = SceneManager.GetSceneByName(sceneName);
        if (s.IsValid()) SceneManager.SetActiveScene(s);
    }
}

