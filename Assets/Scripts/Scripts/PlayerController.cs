using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
   // private Rigidbody playerRigidbody;
    public float speed = 8f;

    void Start()
    {
        // 게임 오브젝트에서 Rigidbody 컴포넌트를 찾아 playerRigidbody에 할당
       // playerRigidbody = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
       // float xInput = Input.GetAxis("Horizontal");
       // float zInput = Input.GetAxis("Vertical");

       // float xSpeed = xInput * speed;
       // float zSpeed = zInput * speed;

        //Vector3 속도를 (xSpeed, 0, xSpeed)로 생성
       // Vector3 newVelocity = new Vector3(xSpeed, 0f, zSpeed);
        //리지드바디의 속도에 newVelocity 할당
       // playerRigidbody.velocity = newVelocity;

    }

    public void Die()
    {
        //자신의 게임 오브젝트를 비활성화
        gameObject.SetActive(false);

        //씬에 존재하는  GameManager 타입의 오브젝트를 찾아서 가져오기
        GameManager gameManager = FindObjectOfType<GameManager>();
        //가져온 GameObject 오브젝트의 EndGame() 메서드 실행
        gameManager.EndGame();
    }
}
