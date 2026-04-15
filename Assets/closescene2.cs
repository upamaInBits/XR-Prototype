using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.SceneManagement;
 public class CloseScene2 : MonoBehaviour 
 { 
    public string sceneName = "InfoScene2"; 
    // OK button
    public void Closescene2()
    { DoorProgress.Instance.MarkDoorDone(gameObject.scene.name); 
    SceneManager.UnloadSceneAsync(sceneName); 
    } 
}