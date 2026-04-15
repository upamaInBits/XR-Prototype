using UnityEngine;
using TMPro;

public class InfoBlurb : MonoBehaviour
{
    [TextArea(2, 4)]
    public string message;          // what to show
    public TMP_Text target;         // the TMP text to write into
    public GameObject targetBlock;  // container object to toggle (use DetailText gameObject)

    public void Show()
    {
        if (target) target.text = message;
        if (targetBlock) targetBlock.SetActive(true);
    }
}
