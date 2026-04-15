using UnityEngine;
using TMPro;

public class EyeButtonHandler : MonoBehaviour
{
    public GameObject eyeExplanationText;

    private bool isVisible = false; // to keep track of current state

    public void ToggleEyeExplanation()
    {
        if (eyeExplanationText != null)
        {
            isVisible = !isVisible; // flip between true/false
            eyeExplanationText.SetActive(isVisible);
        }
    }
}
