using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehavior : StateMachineBehaviour
{
    // PUBLIC VARS
    public float moveSpeed = 0f;
    public float distThreshold = 0f;
    [ReadOnly] public Vector2 mousePos = Vector2.zero;

    // PRIVATE VARS
    private GameObject _child = null;
    [SerializeField, ReadOnly] private PlayerData _pData = null;
    private Rigidbody2D rb = null;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_child == null)
            _child = animator.transform.GetChild(0).gameObject;
        if (GameObject.Find("PlayerMaster") != null)
            _pData = GameObject.Find("PlayerMaster").GetComponent<PlayerData>();
        if (rb == null)
            rb = animator.transform.GetChild(0).GetComponent<Rigidbody2D>();
        GetMousePos();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetMousePos();

        if (_pData.isHolding == false)
            animator.SetBool("isHoldingFood", false);
        if (Vector3.Distance(_child.transform.position, mousePos) < distThreshold)
            animator.SetBool("mouseClose", true);
        else
            animator.SetBool("mouseClose", false);

        FollowFood(animator);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       _child.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    void GetMousePos()
    {
        Vector3 getMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector2(getMouse.x, getMouse.y);
    }

    void FollowFood(Animator animator)
    {
        float clampX = mousePos.x - _child.transform.position.x;
        float clampY = mousePos.y - _child.transform.position.y;
        clampX = Mathf.Clamp(clampX, -1f, 1f);
        clampY = Mathf.Clamp(clampY, -1f, 1f);

        Vector2 direction = new Vector2(clampX, clampY);

        rb.AddForce(((direction * moveSpeed) - rb.velocity) * Time.deltaTime, ForceMode2D.Impulse);
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
}
