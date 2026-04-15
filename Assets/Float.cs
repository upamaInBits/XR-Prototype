using UnityEngine;

public class Floaty : MonoBehaviour {
    public Transform visual;               // assign the label mesh/TMP here
    public float amp = 0.02f, freq = 1.2f;
    Vector3 startLocal;

    void Awake() {
        if (visual == null) visual = transform;   // fallback
        startLocal = visual.localPosition;
    }

    void LateUpdate() {
        visual.localPosition = startLocal + Vector3.up * Mathf.Sin(Time.time * freq) * amp;
    }
}


