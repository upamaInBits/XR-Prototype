using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShowDoorHidePuppyAfterAudio : MonoBehaviour
{
    [Header("Targets")]
    public GameObject Door;     // assign Door
    public GameObject Puppy;    // assign Puppy
    public GameObject Door1;     // assign Door

    [Header("Audio")]
    public AudioSource Source;  // assign WelcomeSpeaker

    public float ExtraDelay = 0.2f;

    public ParticleSystem Sparkle;


    void Awake()
    {
        if (Source == null) Source = GetComponent<AudioSource>();

        // Puppy should be visible while audio plays
        if (Puppy != null) Puppy.SetActive(true);

        // Door should appear only after audio
        if (Door != null) Door.SetActive(false);

         // Door should appear only after audio
        if (Door1 != null) Door1.SetActive(false);
    }

    void OnEnable()
    {
        if (Source != null && !Source.isPlaying) Source.Play();
        StartCoroutine(WaitThenSwap());
    }

    System.Collections.IEnumerator WaitThenSwap()
    {
        // Wait until audio is done
        while (Source != null && Source.isPlaying) yield return null;

        if (ExtraDelay > 0f) yield return new WaitForSeconds(ExtraDelay);

        // Show door, hide puppy
        if (Door != null) Door.SetActive(true);
        if (Door1 != null) Door1.SetActive(true);
        if (Puppy != null) Puppy.SetActive(false);
    }

    IEnumerator Start()
    {
        // Puppy is visible at start, door hidden
        Puppy.SetActive(true);
        Door.SetActive(false);
        Door1.SetActive(false);

        // Wait for audio to finish
        yield return new WaitForSeconds(Source.clip.length + ExtraDelay);

        // Play sparkle effect ✨
        if (Sparkle != null)
            Sparkle.Play();

        // Hide puppy + show door
        Puppy.SetActive(false);
        Door.SetActive(true);
        Door1.SetActive(true);
    }
}
