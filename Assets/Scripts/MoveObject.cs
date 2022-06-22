using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MoveObject : MonoBehaviour
{
    public float Aspeed;
    private Joystick controller;
    public VariableJoystick variableJoystick;
    public Transform moving_object;
    public Transform camPivot;

    private void Awake()
    {
        variableJoystick = variableJoystick.GetComponent<VariableJoystick>();
    }
    /*
    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        if (direction == Vector3.zero) return;


        moving_object.rotation = Quaternion.LookRotation(direction);
        moving_object.Translate(direction * Aspeed * Time.fixedDeltaTime);
    }
    */
    
    public void FixedUpdate()
    {
        Vector2 conDir = variableJoystick.Direction;
        if (conDir == Vector2.zero) return;

        float thetaEuler = Mathf.Acos(conDir.y / conDir.magnitude) * (4 / Mathf.PI) * Mathf.Sign(conDir.x);

        Vector3 moveAngle = Vector3.up * (camPivot.transform.rotation.eulerAngles.y + thetaEuler);
        moving_object.rotation = Quaternion.Euler(moveAngle);
        moving_object.Translate(Vector3.forward * Time.fixedDeltaTime * Aspeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finisih")
        {
            SceneManager.LoadScene("main");
        }
        else if(other.tag == "FinisihOne")
        {
            SceneManager.LoadScene("Stage2");
        }
    }

}
