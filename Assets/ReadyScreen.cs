using UnityEngine;
using UnityEngine.SceneManagement;

public class ReadyScreen : MonoBehaviour
{
    [Header("Mini-game scene to load")]
    public string miniGameScene = "ReadyScene_working"; // or WaterGameScene

    public void Play()
    {
        SceneManager.LoadSceneAsync(miniGameScene);
    }
}

