using UnityEngine;
using System.Collections.Generic;
using System.Collections;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class CharacterControls : MonoBehaviour
{

	public float speed = 10.0f;
	public float airVelocity = 8f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	public float jumpHeight = 2.0f;
	public float maxFallSpeed = 20.0f;
	public float rotateSpeed = 25f; //플레이어 회전 속도 향상
	private Vector3 moveDir;
	public GameObject cam;
	private Rigidbody rb;

	private float distToGround;

	private bool canMove = true; //플레이어가 히트를 하지 않은 경우
	private bool isStuned = false;
	private bool wasStuned = false; //플레이어가 다음에 기절하기 전에 기절했다면
	private float pushForce;
	private Vector3 pushDir;
	public bool Jump = false;

	public Vector3 checkPoint;
	private bool slide = false;

	void Start()
	{
		// 지상에 닿다
		distToGround = GetComponent<Collider>().bounds.extents.y;
	}

	bool IsGrounded()
	{
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
	}

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;
		//rb.useGravity = false;

		checkPoint = transform.position;
		Cursor.visible = false;
	}

	void FixedUpdate()
	{
		if (canMove)
		{
			if (moveDir.x != 0 || moveDir.z != 0)
			{
				Vector3 targetDir = moveDir; //캐릭터 방향

				targetDir.y = 0;
				if (targetDir == Vector3.zero)
					targetDir = transform.forward;
				Quaternion tr = Quaternion.LookRotation(targetDir); //문자를 이동할 위치로 회전
				Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, Time.deltaTime * rotateSpeed); //조금씩 캐릭터를 회전시킵니다.
				transform.rotation = targetRotation;
			}

			if (IsGrounded())
			{
				// 얼마나 빨리 움직여야 하는지 계산하십시오.
				Vector3 targetVelocity = moveDir;
				targetVelocity *= speed;

				// 목표 속도에 도달하는 힘을 가합니다.
				Vector3 velocity = rb.velocity;
				if (targetVelocity.magnitude < velocity.magnitude) //내가 캐릭터를 느리게 한다면.
				{
					targetVelocity = velocity;
					rb.velocity /= 1.1f;
				}
				Vector3 velocityChange = (targetVelocity - velocity);
				velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
				velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
				velocityChange.y = 0;
				if (!slide)
				{
					if (Mathf.Abs(rb.velocity.magnitude) < speed * 1.0f)
						rb.AddForce(velocityChange, ForceMode.VelocityChange);
				}
				else if (Mathf.Abs(rb.velocity.magnitude) < speed * 1.0f)
				{
					rb.AddForce(moveDir * 0.15f, ForceMode.VelocityChange);
					//Debug.Log(rb.velocity.magnitude);
				}

				//Jump
				if (IsGrounded() && Jump == true)
				{
					rb.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
					Jump = false;
				}
                else
                {
					rb.velocity = new Vector3(velocity.x, 0, velocity.z);
				}
			}
			else
			{
				if (!slide)
				{
					Vector3 targetVelocity = new Vector3(moveDir.x * airVelocity, rb.velocity.y, moveDir.z * airVelocity);
					Vector3 velocity = rb.velocity;
					Vector3 velocityChange = (targetVelocity - velocity);
					velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
					velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
					rb.AddForce(velocityChange, ForceMode.VelocityChange);
					if (velocity.y < -maxFallSpeed)
						rb.velocity = new Vector3(velocity.x, -maxFallSpeed, velocity.z);
				}
				else if (Mathf.Abs(rb.velocity.magnitude) < speed * 1.0f)
				{
					rb.AddForce(moveDir * 0.15f, ForceMode.VelocityChange);
				}
			}
		}
		else
		{
			rb.velocity = pushDir * pushForce;
		}

		// 우리는 더 많은 튜닝 제어를 위해 수동으로 중력을 가한다.
		rb.AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0));
		//anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxisRaw("Vertical")) + Mathf.Abs(Input.GetAxisRaw("Horizontal"))));
	}

	private void Update()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		Vector3 v2 = v * cam.transform.forward; //카메라를 기준으로 이동할 수직 축
		Vector3 h2 = h * cam.transform.right; //카메라를 기준으로 이동할 수평 축
		moveDir = (v2 + h2).normalized; //진도 1에서 이동하고자 하는 전역 위치

		RaycastHit hit;
		if (Physics.Raycast(transform.position, -Vector3.up, out hit, distToGround + 0.1f))
		{
			if (hit.transform.tag == "Slide")
			{
				slide = true;
			}
			else
			{
				slide = false;
			}
		}
	}

	float CalculateJumpVerticalSpeed()
	{
		// 점프 높이와 중력에 의해 상승 속도를 추론할 수 있습니다.
		// 캐릭터가 정점에 도달하기 위해.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}

	public void HitPlayer(Vector3 velocityF, float time)
	{
		rb.velocity = velocityF;

		pushForce = velocityF.magnitude;
		pushDir = Vector3.Normalize(velocityF);
		StartCoroutine(Decrease(velocityF.magnitude, time));
	}

	public void LoadCheckPoint()
	{
		transform.position = checkPoint;
	}

	private IEnumerator Decrease(float value, float duration)
	{
		if (isStuned)
			wasStuned = true;
		isStuned = true;
		canMove = false;

		float delta = 0;
		delta = value / duration;

		for (float t = 0; t < duration; t += Time.deltaTime)
		{
			yield return null;
			if (!slide) //접지가 미끄러지지 않을 경우 힘을 줄입니다.
			{
				pushForce = pushForce - Time.deltaTime * delta;
				pushForce = pushForce < 0 ? 0 : pushForce;
				//Debug.Log(pushForce);
			}
			//rb.AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0)); //중력 추가
		}

		if (wasStuned)
		{
			wasStuned = false;
		}
		else
		{
			isStuned = false;
			canMove = true;
		}
	}
	public void JumpButton()
	{
		Jump = true;
	}
}




