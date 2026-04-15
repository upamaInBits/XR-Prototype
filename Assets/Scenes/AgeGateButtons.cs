using UnityEngine;
using UnityEngine.SceneManagement;

public class AgeGateButtons : MonoBehaviour
{
    [Header("Too-young message panel/text")]
    public GameObject tooYoungMessage;

    [Header("Next scene if old enough")]
    public string nextSceneName = "back_working_version";  // your hub scene name

    // Call this for ages 6 or younger
    public void OnTooYoungChosen()
    {
        if (tooYoungMessage != null)
            tooYoungMessage.SetActive(true);
    }

    // Call this for ages 7+
    public void OnOldEnoughChosen()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}

