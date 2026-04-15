using UnityEngine;
using UnityEngine.UI;

public class VRColorTest : MonoBehaviour
{
    public Image fillImage;     // drag your Slider Fill here

    private float timer = 0f;
    private bool isGreen = true;

    void Update()
    {
        if (fillImage == null) return;

        timer += Time.deltaTime;

        // Every 2 seconds, flip color
        if (timer >= 2f)
        {
            timer = 0f;

            if (isGreen)
            {
                fillImage.color = Color.red;
                Debug.Log("TEST: Changed to RED");
            }
            else
            {
                fillImage.color = Color.green;
                Debug.Log("TEST: Changed to GREEN");
            }

            isGreen = !isGreen;
        }
    }
}
