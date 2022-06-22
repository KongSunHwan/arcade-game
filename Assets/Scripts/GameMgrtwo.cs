using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMgrtwo : MonoBehaviour
{
    public Text timeText;
    private float time;

    void Start()
    {

    }

    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else if (time < 0)
        {
            SceneManager.LoadScene("Stage1");
        }

        timeText.text = Mathf.Ceil(time).ToString();

    }

    private void Awake()
    {
        time = 10f;
    }
}
