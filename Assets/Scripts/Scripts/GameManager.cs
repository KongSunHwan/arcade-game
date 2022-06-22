using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI관련 라이브러리
using UnityEngine.SceneManagement; //씬 관리 관련 라이브러리

public class GameManager : MonoBehaviour
{
    public GameObject gameoverText;
    public GameObject clearText;
    public Text timeText;
    public Text recordText;
    public Text clear;

    private float surviveTime;//생존 시간
    private bool isGameover;//게임오버 상태

    void Start()
    {
        surviveTime = 0;
        isGameover = false;
    }

    void Update()
    {
        if (!isGameover)
        {
            surviveTime += Time.deltaTime;
            timeText.text = "Time: " + (int)surviveTime;
        }
        else
        {
            SceneManager.LoadScene("main");
        }
        if (surviveTime >= 20)
        {
           clearText.SetActive(true);
           SceneManager.LoadScene("Stage4");
        }
    }

    public void EndGame()
    {
        isGameover = true;
        gameoverText.SetActive(true);
        Time.timeScale = 0;

        float bestTime = PlayerPrefs.GetFloat("BestTime");

        if(surviveTime > bestTime)
        {
            bestTime = surviveTime;

            PlayerPrefs.SetFloat("BestTime", bestTime);
        }
        recordText.text = "Best Time: " + (int)bestTime;
    }
}
