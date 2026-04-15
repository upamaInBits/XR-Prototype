using UnityEngine;

public class HubDoorVisibility : MonoBehaviour
{
    [Header("Must match the doorId used in NewClosePanel, e.g. Door1, Door2, Door3")]
    public string doorId = "Door1";

    void Start()
    {
        if (DoorProgress.Instance == null)
        {
            Debug.LogWarning("[HubDoorVisibility] No DoorProgress found. Showing door by default.");
            return;
        }

        bool done = DoorProgress.Instance.IsDoorDone(doorId);
        gameObject.SetActive(!done);  // hide only if DONE
    }
}
