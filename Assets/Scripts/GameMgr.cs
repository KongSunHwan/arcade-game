using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    public GameObject GameOverText;
    public Text playerGoldText;
    public Text timeText;
    private float time;
    public Text Relsut;
    private int rels;

    public static int getCount = 0;
    public GameObject winText;

    void Start()
    {
        // winText.SetActive(false);
        rels = Random.Range(25, 60);
        //gameoverText.SetActive(false);
    }

    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else if(time < 0)
        {
            SceneManager.LoadScene("main");
            GameOverText.SetActive(true);
        }

        timeText.text = Mathf.Ceil(time).ToString();

        Relsut.text = rels.ToString();

        if (Input.GetMouseButtonDown(0))
        {
            getCount++;
        }
        if (getCount >= rels)
        {
            SceneManager.LoadScene("Stage3");
            winText.SetActive(true);
        }
    }

    private void Awake()
    {
        time = 15f;
    }

}
