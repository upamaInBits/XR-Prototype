using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // <- New Input System

public class WaterFlowerXR : MonoBehaviour
{
    [Header("References")]
    public ParticleSystem water;     // drag WaterParticle here
    public Transform flower;         // drag your Flower root here

    [Header("XR Input Action")]
    // Drag the "Activate" action from Left or Right controller here
    public InputActionReference triggerAction;

    [Header("Growth")]
    public float growPerSecond = 0.25f; // meters per second, uniform
    public float maxScale = 1.8f;       // stop growing around this uniform scale

    void OnEnable()
    {
        if (triggerAction != null) triggerAction.action.Enable();
    }

    void OnDisable()
    {
        if (triggerAction != null) triggerAction.action.Disable();
    }

    void Update()
    {
        bool pressed = triggerAction != null && triggerAction.action.IsPressed();

        if (pressed)
        {
            if (water && !water.isPlaying) water.Play();

            if (flower)
            {
                var s = flower.localScale;
                if (s.x < maxScale)
                {
                    float g = growPerSecond * Time.deltaTime;
                    flower.localScale = s + Vector3.one * g;
                }
            }
        }
        else
        {
            if (water && water.isPlaying) water.Stop();
        }
    }
}
