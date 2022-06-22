using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxJump : MonoBehaviour
{
    //public int jumpState = 0; // 0번 땅바닥, 1번 점프상태, 2번 다운상태
    public enum JUMPSTATE
    {
        IDLE = 0,
        JUMP,
        DOWN
    }
    public float jumpSpeed = 8f;
    public float jumpLimit = 3f;
    public JUMPSTATE jumpState;
    public int jumpCnt = 0;
    public int jumpCntMax = 2;
    void Start()
    {
        
    }


    void Update()
    {
        switch (jumpState)
        {
            case JUMPSTATE.IDLE:
                transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
                jumpCnt = 0;
                jumpLimit = 3f;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    jumpState = JUMPSTATE.JUMP;
                    jumpCnt++;
                }
                break;
            case JUMPSTATE.JUMP:
                transform.Translate(0, jumpSpeed * Time.deltaTime, 0);
                if (Input.GetKeyDown(KeyCode.Space) && jumpCnt < jumpCntMax)
                {
                    jumpLimit += transform.position.y;
                    jumpCnt++;
                }
                if (transform.position.y > jumpLimit)
                {
                    jumpState = JUMPSTATE.DOWN;
                }
                break;
            case JUMPSTATE.DOWN: 
                transform.Translate(0, -jumpSpeed * Time.deltaTime, 0);
                if (Input.GetKeyDown(KeyCode.Space) && jumpCnt < jumpCntMax)
                {
                    jumpLimit += transform.position.y;
                    jumpCnt++;
                    jumpState = JUMPSTATE.JUMP;
                }
                if (transform.position.y < 0.5f)
                {
                    jumpState = JUMPSTATE.IDLE;
                }
                break;
            default:
                break;
        }


    }
    public void Jump()
    {
            if (jumpCnt < jumpCntMax)
        {
            jumpState = JUMPSTATE.JUMP;
            jumpCnt++;
        }

    }
}
