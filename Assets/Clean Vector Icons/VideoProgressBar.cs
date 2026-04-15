using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoProgressBar : MonoBehaviour
{
    public VideoPlayer videoPlayer;   // drag your VideoPlayer here
    public Slider progressSlider;     // drag the Slider here

    void Update()
    {
        if (videoPlayer == null || progressSlider == null)
            return;

        // Safeguard for prepared video
        if (!videoPlayer.isPrepared || videoPlayer.length <= 0)
            return;

        double currentTime = videoPlayer.time;
        double totalTime   = videoPlayer.length;

        // Normal progress, clamped 0–1
        float t = Mathf.Clamp01((float)(currentTime / totalTime));
        progressSlider.value = t;

        // If the video is basically at the end OR has stopped, snap to full
        const double epsilon = 0.1; // 0.1 seconds from the end
        if (!videoPlayer.isPlaying && currentTime >= totalTime - epsilon)
        {
            progressSlider.value = 1f;
        }
    }
}
