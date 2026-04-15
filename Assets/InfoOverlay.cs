using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoOverlay : MonoBehaviour
{
    [Header("Placement")]
    public Transform anchor;
    public bool faceCamera = false;
    public float distance = 2f; // fallback if no anchor

    [Header("Sizing")]
    public float worldWidthMeters = 1.2f;                // how wide on the wall
    public Vector2 canvasSize = new Vector2(1700, 1000); // your UI ref size

    void ApplySize()
    {
        var rt = (RectTransform)transform;
        rt.sizeDelta = canvasSize;
        float s = worldWidthMeters / canvasSize.x;       // pixels → meters
        transform.localScale = Vector3.one * s;
    }

    void OnEnable()
    {
        ApplySize();

        var cam = Camera.main;
        if (anchor)
        {
            transform.position = anchor.position;
            transform.rotation = anchor.rotation;
            if (faceCamera && cam)
                transform.rotation = Quaternion.LookRotation(
                    cam.transform.position - transform.position, Vector3.up);
        }
        else if (cam)
        {
            transform.position = cam.transform.position + cam.transform.forward * distance;
            transform.rotation = Quaternion.LookRotation(
                transform.position - cam.transform.position, Vector3.up);
        }
    }

    public void Close() =>
        SceneManager.UnloadSceneAsync(gameObject.scene.name);
}
