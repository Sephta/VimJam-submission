using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeBehavior : StateMachineBehaviour
{
    public float distThreshold = 0f;
    public float moveSpeed = 0f;

    [ReadOnly] public Vector2 mousePos = Vector2.zero;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetMousePos();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetMousePos();

        if (Vector3.Distance(animator.transform.position, mousePos) < distThreshold)
        {
            Flee(animator);
        }
        else
        {
            animator.SetBool("mouseClose", false);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    void GetMousePos()
    {
        Vector3 getMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector2(getMouse.x, getMouse.y);
    }

    void Flee(Animator animator)
    {
        Vector2 direction = new Vector2(animator.transform.position.x - mousePos.x, animator.transform.position.y - mousePos.y);

        float clampX = direction.x;
        float clampY = direction.y;
        clampX = Mathf.Clamp(clampX, -1f, 1f);
        clampY = Mathf.Clamp(clampY, -1f, 1f);

        Rigidbody2D rb = animator.gameObject.GetComponent<Rigidbody2D>();

        rb.AddForce(((direction * moveSpeed) - rb.velocity) * Time.deltaTime, ForceMode2D.Impulse);
    }
}
