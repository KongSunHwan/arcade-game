using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public Animator animator;
    private float h;
    private float v;
    bool Run;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        animator.SetFloat("h", h);
        animator.SetFloat("v", v);

        // Debug.Log(v);
       if (Input.GetMouseButtonDown(0))
        {
            if (!Run)
            {
                Run = true;
                animator.SetBool("RUN", true);
                return;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            {
                Run = false;
                animator.SetBool("RUN", false);
                return;
            }
        }


    }
    public void OnJump()
    {
        animator.Play("JumpOneTake", -1, 0);
    }
}
