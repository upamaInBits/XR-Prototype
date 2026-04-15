using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaterFlower : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public ParticleSystem water;   // drag WaterPS here
    public Transform flower;       // drag your flower here

    [Header("Growth")]
    public float growPerSecond = 0.15f; // how fast the flower grows
    public float maxScale = 1.8f;       // stop at this size (uniform x)

    void Update()
    {
        bool triggerDown =
            Input.GetButton("Fire1")                                 // Legacy Input ("Fire1" often maps to primary trigger/mouse)
            || Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") > 0.1f   // Quest legacy axis
            || Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger") > 0.1f;

        if (triggerDown)
        {
            if (!water.isPlaying) water.Play();

            // Grow uniformly until maxScale
            if (flower.localScale.x < maxScale)
            {
                float d = growPerSecond * Time.deltaTime;
                flower.localScale += new Vector3(d, d, d);
            }
        }
        else
        {
            if (water.isPlaying) water.Stop();
        }
    }
}

