using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RevealDoorsAfterAudio : MonoBehaviour
{
    [Header("Targets")]
    public GameObject[] DoorsToReveal;   // assign ALL doors that should appear after audio
    public GameObject Puppy;

    [Header("Audio")]
    public AudioSource Source;           // WelcomeSpeaker
    public float ExtraDelay = 0.2f;
    public ParticleSystem Sparkle;       // optional burst when revealing

    void Awake()
    {
        if (!Source) Source = GetComponent<AudioSource>();

        if (Puppy) Puppy.SetActive(true);
        foreach (var d in DoorsToReveal)
            if (d) d.SetActive(false);   // keep every door hidden at start
    }

    void OnEnable()
    {
        if (Source && !Source.isPlaying) Source.Play();
        StartCoroutine(Reveal());
    }

    IEnumerator Reveal()
    {
        while (Source && Source.isPlaying) yield return null;
        if (ExtraDelay > 0) yield return new WaitForSeconds(ExtraDelay);

        if (Sparkle) Sparkle.Play();
        foreach (var d in DoorsToReveal)
            if (d) d.SetActive(true);

        if (Puppy) Puppy.SetActive(false);
    }
}
