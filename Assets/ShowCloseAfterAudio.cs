using System.Collections;
using UnityEngine;

public class ShowCloseAfterAudio : MonoBehaviour
{
    [Header("UI")]
    public GameObject closeButton;          // the Close button GameObject (not just the Button component)

    [Header("Narration to wait for")]
    public AudioSource[] narrationSources;  // one or more AudioSources that must finish

    [Tooltip("Optional extra delay after audio stops, in seconds")]
    public float extraDelay = 0f;

    [Tooltip("If true and a source isn’t already playing, start it automatically")]
    public bool autoPlayIfIdle = false;

    void Awake()
    {
        if (closeButton) closeButton.SetActive(false);
    }

    void OnEnable()
    {
        StartCoroutine(WaitForNarrationThenShow());
    }

    IEnumerator WaitForNarrationThenShow()
    {
        // If nothing assigned, just show immediately
        if (narrationSources == null || narrationSources.Length == 0)
        {
            yield return null;
            if (closeButton) closeButton.SetActive(true);
            yield break;
        }

        // Optionally start any idle sources
        if (autoPlayIfIdle)
        {
            foreach (var s in narrationSources)
            {
                if (s && s.clip && !s.isPlaying) s.Play();
            }
        }

        // Wait until all assigned sources are NOT playing
        while (AnyPlaying(narrationSources))
            yield return null;

        if (extraDelay > 0f)
            yield return new WaitForSeconds(extraDelay);

        if (closeButton) closeButton.SetActive(true);
    }

    static bool AnyPlaying(AudioSource[] sources)
    {
        foreach (var s in sources)
        {
            if (s && s.isPlaying) return true;
        }
        return false;
    }
}

