using UnityEngine;

public class CameraButtonHandler : MonoBehaviour
{
    public GameObject cameraText;
    private bool isVisible = false;

    public void ToggleCamera()
    {
        isVisible = !isVisible;
        cameraText.SetActive(isVisible);
    }
}
