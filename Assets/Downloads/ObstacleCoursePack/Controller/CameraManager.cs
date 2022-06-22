using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	public float followSpeed = 3; //카메라가 우리를 따라오는 속도
	public float mouseSpeed = 2; //마우스를 사용하여 카메라를 회전하는 속도
	public float controllerSpeed = 5; //조이스틱을 사용하여 카메라를 회전하는 속도
	public float cameraDist = 3; //카메라가 위치한 거리

	public Transform target; //카메라가 따라가는 플레이어

	[HideInInspector]
	public Transform pivot; //카메라가 회전하는 피벗(카메라와 캐릭터 사이의 원하는 거리)
	[HideInInspector]
	public Transform camTrans; //카메라 위치

	float turnSmoothing = .1f; //모든 카메라 이동을 부드럽게 합니다(카메라가 조이스틱으로 표시된 회전에 도달하는 데 걸리는 시간).
	public float minAngle = -35; //카메라가 도달할 수 있는 최소 각도
	public float maxAngle = 35; //카메라가 도달할 수 있는 최대 각도

	float smoothX;
	float smoothY;
	float smoothXvelocity;
	float smoothYvelocity;
	public float lookAngle; //Y축에서 카메라의 각도
	public float tiltAngle; //카메라의 위/아래 각도

	public void Init()
	{
		camTrans = Camera.main.transform;
		pivot = camTrans.parent;
	}

	void FollowTarget(float d)
	{ //카메라가 플레이어를 따라가도록 하는 기능
		float speed = d * followSpeed; //fps에 관계없이 속도 설정
		Vector3 targetPosition = Vector3.Lerp(transform.position, target.position, speed); //속도를 보간하는 플레이어에게 카메라를 더 가까이 가져갑니다(0.5반, 1개 모두).
		transform.position = targetPosition; //카메라 위치 업데이트
	}

	void HandleRotations(float d, float v, float h, float targetSpeed)
	{ //카메라를 올바르게 회전시키는 기능
		if (turnSmoothing > 0)
		{
			smoothX = Mathf.SmoothDamp(smoothX, h, ref smoothXvelocity, turnSmoothing); //시간이 지남에 따라 원하는 목표를 향해 서서히 값을 변경합니다.
			smoothY = Mathf.SmoothDamp(smoothY, v, ref smoothYvelocity, turnSmoothing);
		}
		else
		{
			smoothX = h;
			smoothY = v;
		}

		tiltAngle -= smoothY * targetSpeed; //카메라가 이동하는 각도를 업데이트합니다.
		tiltAngle = Mathf.Clamp(tiltAngle, minAngle, maxAngle); //최대값 및 최소값에 대한 한계
		pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0); //위쪽/아래쪽 각도 수정

		lookAngle += smoothX * targetSpeed; //y 단위의 회전 각도를 부드럽게 업데이트합니다
		transform.rotation = Quaternion.Euler(0, lookAngle, 0); //각도 적용

	}

	private void FixedUpdate()
	{//조이스틱/마우스를 기반으로 카메라를 올바르게 회전시키고 플레이어를 따라가는 기능(델타 시간은 fps와 독립적으로 전송됨)
		float h = Input.GetAxis("Mouse X");
		float v = Input.GetAxis("Mouse Y");

		//float c_h = Input.GetAxis("RightAxis X");
		//float c_v = Input.GetAxis("RightAxis Y");

		float targetSpeed = mouseSpeed;
		
		/*
		if (c_h != 0 || c_v != 0)
		{ //조이스틱을 사용할 경우 덮어쓰기
			h = c_h;
			v = -c_v;
			targetSpeed = controllerSpeed; 
		}
		*/
		

		FollowTarget(Time.deltaTime); //팔로우 플레이어
		HandleRotations(Time.deltaTime, v, h, targetSpeed); //카메라를 회전합니다.
	}

	private void LateUpdate()
	{
		//여기서 벽을 감지하여 카메라를 가까이 오게 하는 코드를 시작합니다.
		float dist = cameraDist + 1.0f; // 카메라와의 거리 + 1.0으로 카메라가 멀리 있는 것을 치면 카메라가 1 유닛을 점프하지 않습니다.
		Ray ray = new Ray(camTrans.parent.position, camTrans.position - camTrans.parent.position);// 목표물에서 카메라로 광선을 쏘아 올립니다.
		RaycastHit hit;
		// read from the taret to the targetPosition;
		if (Physics.Raycast(ray, out hit, dist))
		{
			if (hit.transform.tag == "Wall")
			{
				// store the distance;
				dist = hit.distance - 0.25f;
			}
		}
		// check if the distance is greater than the max camera distance; // 거리가 최대 카메라 거리보다 큰지 확인합니다
		if (dist > cameraDist) dist = cameraDist;
		camTrans.localPosition = new Vector3(0, 0, -dist);
	}

	public static CameraManager singleton; //다른 스크립트에서 CameraManager.singleton을 호출할 수 있습니다.
	void Awake()
	{
		singleton = this; //Self-assigns
		Init();
	}

}
