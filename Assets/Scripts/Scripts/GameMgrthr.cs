using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMgrthr : MonoBehaviour
{
    public GameObject gameoverText;
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
            gameoverText.SetActive(true);
            SceneManager.LoadScene("main");
        }

        timeText.text = Mathf.Ceil(time).ToString();

    }

    private void Awake()
    {
        time = 60f;
    }
}
