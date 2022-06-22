using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textBlink_time : MonoBehaviour
{
    Text flashingText;

    private void Start()
    {
        flashingText = GetComponent<Text>();
        StartCoroutine(BlinkText());
    }

    public IEnumerator BlinkText()
    {
        while (true)
        {
            flashingText.text = "";
            yield return new WaitForSeconds(.3f);
            flashingText.text = "¢Ø   ¢Ö";
            yield return new WaitForSeconds(.3f);
        }
    }
}
