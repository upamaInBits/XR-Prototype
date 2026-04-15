using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.SceneManagement; 
public class CloseSelfOverlay1 : MonoBehaviour
 { 
    public string sceneName = "InfoScene1";
     // Called by the OK button 
public void CloseInfo2() 
    { 
        DoorProgress.Instance.MarkDoorDone(gameObject.scene.name); 
    SceneManager.UnloadSceneAsync(sceneName); // close the overlay
    }
  }
