using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PuppyIdle : MonoBehaviour
{
    public Transform lookTarget;
    public float bobAmplitude = 0.03f;
    public float bobSpeed = 2f;
    Vector3 basePos;

    void Start() 
    { 
        basePos = transform.position; 
    }

    void Update()
    {
        // gentle bounce
        transform.position = basePos + Vector3.up * (Mathf.Sin(Time.time * bobSpeed) * bobAmplitude);

        // look at player (ignore tilt)
        if (lookTarget)
        {
            Vector3 target = lookTarget.position;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }
}

