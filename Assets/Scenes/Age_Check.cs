using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AgeCheck : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField birthYearInput;
    public GameObject tooYoungMessage;

    [Header("Next Scene")]
    public string nextSceneName = "working_version"; // your puppy+doors scene name

    public int minimumAge = 6; // app is for 6 and older

    public void CheckAge()
    {
        // Try to read what they typed
        if (int.TryParse(birthYearInput.text, out int birthYear))
        {
            int currentYear = System.DateTime.Now.Year;
            int age = currentYear - birthYear;

            Debug.Log("Calculated age: " + age);

            if (age < minimumAge)
            {
                // Too young: show message, stay here
                if (tooYoungMessage != null)
                    tooYoungMessage.SetActive(true);
            }
            else
            {
                // Old enough: go to your main hub scene
                SceneManager.LoadScene(nextSceneName);
            }
        }
        else
        {
            Debug.Log("Invalid year input.");
            // (Optional) You could show a "Please type a valid year" message.
        }
    }
}

