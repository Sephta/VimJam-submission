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
    private GameObject _child = null;
    private AIController _aic = null;
    private Rigidbody2D _crb = null;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_child == null)
           _child = animator.transform.GetChild(0).gameObject;
        if (_aic == null)
            _aic = animator.transform.gameObject.GetComponent<AIController>();
        if (_crb == null)
            _crb = _child.GetComponent<Rigidbody2D>();
        
       FindNewDestination(animator);
       GetMousePos();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetMousePos();

        if (Vector3.Distance(_child.transform.position, mousePos) < distThreshold)
        {
            animator.SetBool("mouseClose", true);
        }

        if (Vector3.Distance(_child.transform.position, currDestination) < 1f || _aic.colStatus)
        {
            FindNewDestination(animator);
            animator.transform.gameObject.GetComponent<AIController>().colStatus = false;
        }
        else
        {
            // Vector3 pos = Vector3.MoveTowards(_child.transform.position, currDestination, moveSpeed * Time.deltaTime);
            // _crb.MovePosition(pos);

            Vector2 direction = new Vector2(_child.transform.position.x - currDestination.x, _child.transform.position.y - currDestination.y);
            
            float clampX = direction.x;
            float clampY = direction.y;
            clampX = Mathf.Clamp(clampX, -1f, 1f);
            clampY = Mathf.Clamp(clampY, -1f, 1f);

            _crb.AddForce(((direction * moveSpeed * -1f) - _crb.velocity) * Time.deltaTime, ForceMode2D.Impulse);
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
        Vector3 randomOffset = new Vector2(Random.Range(-xRange, xRange), Random.Range(-yRange, yRange));

        currDestination = _child.transform.position + randomOffset;
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
