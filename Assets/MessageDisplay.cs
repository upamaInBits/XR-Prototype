using UnityEngine;
using TMPro;
using System.Collections;

public class MessageDisplay : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI messageText;

    [Header("Timing")]
    public float messageDuration = 2f;
    [TextArea]
    public string defaultMessage = "hello!";

    private Coroutine currentRoutine;

    void Start()
    {
        // show default at the start
        if (messageText != null)
            messageText.text = defaultMessage;
    }

    public void ShowTemporary(string text)
    {
        if (messageText == null) return;

        // restart timer if called again
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(ShowTempRoutine(text));
    }

    private IEnumerator ShowTempRoutine(string text)
    {
        messageText.text = text;
        yield return new WaitForSeconds(messageDuration);
        messageText.text = defaultMessage;
        currentRoutine = null;
    }
}
