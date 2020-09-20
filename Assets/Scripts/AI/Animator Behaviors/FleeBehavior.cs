using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeBehavior : StateMachineBehaviour
{
    public float distThreshold = 0f;
    public float moveSpeed = 0f;

    [ReadOnly] public Vector2 mousePos = Vector2.zero;

    [SerializeField, ReadOnly] private Vector2 direction = Vector2.zero;

    private GameObject _child = null;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_child == null)
            _child = animator.transform.GetChild(0).gameObject;

        GetMousePos();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetMousePos();

        if (direction.x >= 0)
            _child.GetComponent<SpriteRenderer>().flipX = false;
        else
            _child.GetComponent<SpriteRenderer>().flipX = true;

        if (Vector3.Distance(_child.transform.position, mousePos) < distThreshold)
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
        _child.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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
        float clampX = _child.transform.position.x - mousePos.x;
        float clampY = _child.transform.position.y - mousePos.y;
        clampX = Mathf.Clamp(clampX, -1f, 1f);
        clampY = Mathf.Clamp(clampY, -1f, 1f);

        direction = new Vector2(clampX, clampY);

        Rigidbody2D rb = _child.GetComponent<Rigidbody2D>();

        rb.AddForce(((direction * moveSpeed) - rb.velocity) * Time.deltaTime, ForceMode2D.Impulse);
    }
}
