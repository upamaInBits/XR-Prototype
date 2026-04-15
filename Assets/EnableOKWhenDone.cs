using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class EnableOKWhenDone : MonoBehaviour
{
    public VideoPlayer videoPlayer;  
    public Button okButton;           

    void Start()
    {
        okButton.interactable = false;        // lock it at start
        videoPlayer.loopPointReached += OnVideoFinished;  
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        okButton.interactable = true;         // unlock when done
    }
}
