using UnityEngine;
using UnityEngine.UI;

public class UIButtonClickSound : MonoBehaviour
{
    public AudioSource uiAudioSource;
    public AudioClip clickSound;

    void Start()
    {
        // Connect the button's OnClick to PlayClick
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(PlayClick);
        }
    }

    void PlayClick()
    {
        if (uiAudioSource != null && clickSound != null)
        {
            uiAudioSource.PlayOneShot(clickSound);
        }
    }
}
