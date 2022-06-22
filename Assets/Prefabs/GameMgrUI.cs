using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMgrUI : MonoBehaviour
{
    public bool GameOver = false;
    public GameObject gameOverText;
    void Start()
    {
        GameOver = false;
        Time.timeScale = 1;
    }

    void Update()
    {
           // if (GameOver == true)
       // {
           // Debug.Log("GameOver!!!!!");
          //  gameOverText.SetActive(GameOver);
           // Time.timeScale = 0;
      //  }
       // if (Input.GetMouseButton(0))
       // {
          //  if (GameOver == true)
          //  {
               // Application.LoadLevel(1);
          //  }
       // }
    }
    public void ChangeGameScene()
    {
        SceneManager.LoadScene("main");
    }

    public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
Application.Quit();
#endif

    }
    public void OnClickNewGame()
    {
        Application.LoadLevel(0);
        Time.timeScale = 1;
    }
    public void OnclickGameGo()
    {
        Time.timeScale = 1;
    }
    public void OnclickReGameGo()
    {
        Application.LoadLevel(1);
        Time.timeScale = 1;
    }

}
