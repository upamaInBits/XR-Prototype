using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;        // new Input System

public class WaterFlowerXR2 : MonoBehaviour
{
    [Header("Scene refs")]
    public ParticleSystem water;      // WaterParticle (child of RightHand)
    public Transform flower;   
    public Transform rightHandController;
    public Transform leftHandController;
       // your flower root (the whole plant)

    [Header("XR Trigger Actions (drag from controllers)")]
    public InputActionProperty rightTrigger; // RightHand Controller -> Select Action
    public InputActionProperty leftTrigger;  // LeftHand Controller  -> Select Action

    [Header("Scene refs")]
    public ParticleSystem sparkle;   // ✨ New: for magical effect


    [Header("Growth settings")]
    public float growPerSecond = 0.25f;      // units per second
    public float shrinkPerSecond = 0.25f;
    public float minScale = 0.5f;            // don’t go smaller than this
    public float maxScale = 2.0f;            // don’t go larger than this

    void OnEnable()
    {
        // Make sure actions are enabled
        rightTrigger.action.Enable();
        leftTrigger.action.Enable();
        if (water) water.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

void Update()
{
    bool growPressed = rightTrigger.action.IsPressed();
    bool shrinkPressed = leftTrigger.action.IsPressed();

    // Play water only when growing (right trigger)
    if (water)
    {
        if (growPressed && !water.isPlaying)
            water.Play();

        if (!growPressed && water.isPlaying)
            water.Stop();
    }

    if (sparkle)
    {
        if (growPressed && !sparkle.isPlaying)
            sparkle.Play();
        else if (!growPressed && sparkle.isPlaying)
            sparkle.Stop();
    }

    // Apply scale
    if (flower)
    {
        float delta = 0f;

        if (growPressed)
            delta += growPerSecond * Time.deltaTime;

        if (shrinkPressed)
            delta -= shrinkPerSecond * Time.deltaTime;

        if (Mathf.Abs(delta) > 0f)
        {
            Vector3 s = flower.localScale + Vector3.one * delta;
            float clamped = Mathf.Clamp(s.x, minScale, maxScale);
            flower.localScale = new Vector3(clamped, clamped, clamped);
        }
    }
}
}
