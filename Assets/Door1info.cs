using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadOverlayScene : MonoBehaviour
{
    public string sceneName = "InfoScene1"; // set per-door in Inspector
    public float hideDelay = 0.05f;         // small delay so VFX can play before hiding

    public ParticleSystem burst;            // optional confetti on click
    public GameObject doorVisualRoot;       // the door parent to hide after click

    public void Go()
    {
        StartCoroutine(PlayThenLoad());
    }

    IEnumerator PlayThenLoad()
    {
        if (burst) burst.Play();
        // load overlay additively
        var op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        // optional short delay, then hide the door so it “disappears”
        if (hideDelay > 0 && doorVisualRoot)
         { yield return new WaitForSeconds(hideDelay); 
         doorVisualRoot.SetActive(false); }

       
        while (!op.isDone) yield return null;
        // optional: set overlay scene active for its UI
        var s = SceneManager.GetSceneByName(sceneName);
        if (s.IsValid()) SceneManager.SetActiveScene(s);

        
    }
}
