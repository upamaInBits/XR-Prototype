using UnityEngine;

public class PotAutoSnap : MonoBehaviour
{
    [Header("Where can this pot snap to?")]
    [SerializeField] public Transform[] snapPoints; 
    public float snapRadius = 0.4f;

    [Header("Lock after snapping?")]
    public bool lockAfterSnap = true;

    private bool hasSnapped = false;

    void Update()
    {
        if (hasSnapped || snapPoints == null || snapPoints.Length == 0)
            return;

        foreach (Transform p in snapPoints)
        {
            if (p == null) continue;

            float dist = Vector3.Distance(transform.position, p.position);
            if (dist < snapRadius)
            {
                SnapToPoint(p);
                break;
            }
        }
    }

    void SnapToPoint(Transform p)
    {
        hasSnapped = true;

        transform.position = p.position;
        transform.rotation = p.rotation;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null && lockAfterSnap)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        Debug.Log("[PotAutoSnap] Snapped to " + p.name);
    }
}
