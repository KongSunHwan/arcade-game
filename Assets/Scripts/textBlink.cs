using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class textBlink : MonoBehaviour
{
    Text flashingText;

    private void Start()
    {
        flashingText = GetComponent<Text>();
        StartCoroutine(BlinkText());
    }

    public IEnumerator BlinkText()
    {
        while(true)
        {
            flashingText.text = "";
            yield return new WaitForSeconds(.3f);
            flashingText.text = "터치하면 시작합니다.";
            yield return new WaitForSeconds(.3f);
        }
    }
}
