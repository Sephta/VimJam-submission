using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehavior : StateMachineBehaviour
{
    // PUBLIC VARS
    [ReadOnly] public Vector2 currDestination = Vector2.zero;
    public float moveSpeed = 0f;
    public float xRange = 0f;
    public float yRange = 0f;

    public float distThreshold = 0f;
    [ReadOnly] public Vector2 mousePos = Vector2.zero;

    // PRIVATE VARS

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       FindNewDestination(animator);
       GetMousePos();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetMousePos();

        if (Vector3.Distance(animator.transform.position, mousePos) < distThreshold)
        {
            animator.SetBool("mouseClose", true);
        }

        bool status = animator.transform.gameObject.GetComponent<AIController>().colStatus;
        if (Vector3.Distance(animator.transform.position, currDestination) < 0.4f || status)
        {
            FindNewDestination(animator);
            animator.transform.gameObject.GetComponent<AIController>().colStatus = false;
        }
        else
        {
            Vector3 position = Vector3.MoveTowards(animator.transform.position, currDestination, moveSpeed * Time.deltaTime);
            animator.transform.gameObject.GetComponent<Rigidbody2D>().MovePosition(position);
        }
    }

    // override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
       
    // }

    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}


    // Bounds: y -> (-0.5, -7.5), x -> (-1.5, -15.5)
    private void FindNewDestination(Animator animator)
    {
        Transform anim_t = animator.transform;
        Vector3 randomOffset = new Vector2(Random.Range(-xRange, xRange), Random.Range(-yRange, yRange));

        currDestination = anim_t.position + randomOffset;
        float clampX = currDestination.x;
        float clampY = currDestination.y;
        clampX = Mathf.Clamp(clampX, -15.5f, -1.5f);
        clampY = Mathf.Clamp(clampY, -7.5f, -0.5f);

        currDestination = new Vector3(clampX, clampY, 0f);
    }

    void GetMousePos()
    {
        Vector3 getMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector2(getMouse.x, getMouse.y);
    }
}
