using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToggle : MonoBehaviour
{
    public Transform hinge;      // Assign the door mesh or parent pivot
    public float openAngle = 90f;
    public float speed = 6f;

    bool isOpen = false;
    Quaternion closedRot, openRot;

    void Start()
    {
        if (hinge == null)
            hinge = transform;

        closedRot = hinge.localRotation;
        openRot = Quaternion.Euler(0, openAngle, 0) * closedRot;
    }

    void Update()
    {
        Quaternion target = isOpen ? openRot : closedRot;
        hinge.localRotation = Quaternion.Slerp(hinge.localRotation, target, Time.deltaTime * speed);
    }

    // Call this when clicked
    public void Toggle()
    {
        isOpen = !isOpen;
    }
}
