using UnityEngine;

public class PotSnapToTable : MonoBehaviour
{
    public bool isLocked = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isLocked) return;

        // Did we enter a snap zone on a table?
        if (other.CompareTag("PotSnapZone"))
        {
            // Stop physics
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
            }

            // Snap to the center of that zone
            transform.position = other.transform.position;
            transform.rotation = other.transform.rotation;

            isLocked = true;

            // OPTIONAL: later we can play a "Great job!" audio here
            // AudioSource audio = GetComponent<AudioSource>();
            // if (audio && placedClip) audio.PlayOneShot(placedClip);
        }
    }
}
