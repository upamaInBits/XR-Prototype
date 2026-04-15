using UnityEngine;
using UnityEngine.Video;

public class VideoControls : MonoBehaviour
{
    public VideoPlayer player;

    void Awake()
    {
        if (!player) player = GetComponent<VideoPlayer>();
        player.audioOutputMode = VideoAudioOutputMode.AudioSource;
        player.EnableAudioTrack(0, true);
        player.SetTargetAudioSource(0, GetComponent<AudioSource>());
    }

    public void PlayVideo()
    {
        if (!player.isPrepared)
        {
            player.Prepare();
            player.prepareCompleted += _ => player.Play();
        }
        else player.Play();
    }

    public void PauseVideo()
    {
        player.Pause();
    }

    public void RestartVideo()
    {
        player.time = 0;
        player.Play();
    }
}

