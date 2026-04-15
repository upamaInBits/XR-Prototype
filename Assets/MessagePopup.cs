using UnityEngine;
using TMPro;
using System.Collections;

public class MessagePopup : MonoBehaviour
{
    public static MessagePopup Instance;

    public TextMeshProUGUI messageText;
    public float displayTime = 2f;

    void Awake()
    {
        Instance = this;
     //   gameObject.SetActive(false);
    }

    public void ShowMessage(string msg)
    {
        StopAllCoroutines();
        StartCoroutine(ShowRoutine(msg));
    }

    private IEnumerator ShowRoutine(string msg)
    {
        messageText.text = msg;
        gameObject.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        gameObject.SetActive(false);
    }
}
