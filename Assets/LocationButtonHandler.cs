using UnityEngine;

public class LocationButtonHandler : MonoBehaviour
{
    public GameObject locationText;
    private bool isVisible = false;

    public void ToggleLocation()
    {
        isVisible = !isVisible;
        locationText.SetActive(isVisible);
    }
}
