using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Change : MonoBehaviour
{
    public void SceneChange()
    {
            SceneManager.LoadScene("tutorial");
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SceneManager.LoadScene("tutorial");
        }
    }
}
