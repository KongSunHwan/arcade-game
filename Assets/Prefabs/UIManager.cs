using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   // public Slider playerHP;
   // public Text itemCounter;
  //  public PlayerController playerController;
  //  public Slider boosterGage;
    public GameObject popUpPanel;
    public Toggle pauseBtn;
    public Slider bgmSlider;
    public Slider EffectSlider;
    public AudioSource bgm;
    public AudioSource[] clip;
    


    void Start()
    {
       // playerHP = GameObject.Find("playerHP").GetComponent<Slider>();
       // playerController = GameObject.Find("Player").GetComponent<PlayerController>();
       // itemCounter = GameObject.Find("itemCounter").GetComponent<Text>();
      //  boosterGage = GameObject.Find("BoosterGage").GetComponent<Slider>();

    }
    public void UpdateSound()
    {
        for(int i = 0; i < clip.Length; i++)
        {
            clip[i].volume = EffectSlider.value;
        }
    }

    // Update is called once per frame
    void Update()
    {
       // playerHP.value = (float)(playerController.playerHP) / (float)(playerController.playerMaxHp);
        //Debug.Log((float)(playerController.playerHP) / (float)(playerController.playerMaxHp));
        //itemCounter.text = "Á¡¼ö :" + playerController.itemCnt;
       // boosterGage.value = (float)(playerController.boosterCnt) / (float)(playerController.boosterMax);
        bgm.volume = bgmSlider.value;
    }

    public void PopUpPanelOnOff()
    {
        if(pauseBtn.isOn == false)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
        popUpPanel.SetActive(pauseBtn.isOn);
    }
}
