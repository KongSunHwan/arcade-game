using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialsItemControl : GameMgr
{
    
    public enum ItemType
    {
        Touch
    }

    [SerializeField] [Header("�����ϱ� ������ ����")] ItemType itemType;
    [SerializeField] [Header("����� �Է� ������ ����ð�")] float timeToInput;
    [SerializeField] [Header("����� �Է� ���� ǥ���� ���ӿ�����Ʈ")] GameObject gameObjectToShow;
    [SerializeField] Text text1;

    bool isReadyToInput = false;
    //private GameMgr script;

    private void Start()
    {
        //GameObject.Find("Test").GetComponent<GameMgr>();

    }
    private void OnEnable()
    {

        Invoke("ShowGameObject", timeToInput);

    }
    // Update is called once per frame
    void Update()
    {
        // �Է´�� ���°� �Ǹ� ��ġ�� �Է� �޴´�.
        if (isReadyToInput)
        {
            if (itemType == ItemType.Touch)
            {
                // �Է��� �ϸ� ��� ����
                if (Input.GetMouseButtonDown(0)) //GetMouseButtonDown
                {
                    Run();
                }

            }

        }
        text1.text = "0";
        getCount = 0;

    }

    virtual protected void Run()
    {
        if (gameObjectToShow == null)
            return;

        // ǥ�� item ��Ȱ��ȭ �ϰ�
        gameObjectToShow.SetActive(false);

        // ���� ������ Ȱ��ȭ
        TutorialsManager parentTutorialsManager = parentTutorialsManager = transform.parent.GetComponent<TutorialsManager>();
        if (parentTutorialsManager != null)
        {
            parentTutorialsManager.ActiveNextItem();
        }

        Time.timeScale = 1.0f;

    }

    void ShowGameObject()
    {
        isReadyToInput = true;
        Time.timeScale = 0.0f;
        if (gameObjectToShow == null)
            return;
        gameObjectToShow.SetActive(true);
    }


}
