using UnityEngine;
using System.Collections;

public class ShrinkWiggle : MonoBehaviour
{
    public float wiggleScale = 0.05f;   // how much to scale
    public float wiggleSpeed = 8f;      // how fast to wiggle

    public void Wiggle()
    {
        StopAllCoroutines();
        StartCoroutine(WiggleRoutine());
    }

    private IEnumerator WiggleRoutine()
    {
        Vector3 original = transform.localScale;

        float t = 0f;
        while (t < 0.25f)   // short wiggle
        {
            t += Time.deltaTime * wiggleSpeed;

            float stretch = 1f + Mathf.Sin(t * Mathf.PI * 2f) * wiggleScale;

            transform.localScale = original * stretch;
            transform.localRotation = Quaternion.identity;   // keep perfectly upright

            yield return null;
        }

        transform.localScale = original;
        transform.localRotation = Quaternion.identity;
    }
}


