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
    [SerializeField, ReadOnly] private Vector2 direction = Vector2.zero;

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

        if (direction.x >= 0)
            _child.GetComponent<SpriteRenderer>().flipX = true;
        else
            _child.GetComponent<SpriteRenderer>().flipX = false;

        if (Vector3.Distance(_child.transform.position, mousePos) < distThreshold)
        {
            animator.SetBool("mouseClose", true);
        }

        if (Vector3.Distance(_child.transform.position, currDestination) < 2f || _aic.colStatus)
        {
            FindNewDestination(animator);
            animator.transform.gameObject.GetComponent<AIController>().colStatus = false;
        }
        else
        {
            float clampX = _child.transform.position.x - currDestination.x;
            float clampY = _child.transform.position.y - currDestination.y;
            clampX = Mathf.Clamp(clampX, -1f, 1f);
            clampY = Mathf.Clamp(clampY, -1f, 1f);

            direction = new Vector2(clampX, clampY);

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


    private Vector2 CalculateDirection(Vector2 destination)
    {
        float clampX = _child.transform.position.x - destination.x;
        float clampY = _child.transform.position.y - destination.y;
        clampX = Mathf.Clamp(clampX, -1f, 1f);
        clampY = Mathf.Clamp(clampY, -1f, 1f);

        Vector2 result = new Vector2(clampX, clampY);

        return result;
    }

    // Bounds: y -> (-0.5, -7.5), x -> (-1.5, -15.5)
    private void FindNewDestination(Animator animator)
    {
        Vector3 randomOffset = new Vector2(Random.Range(-xRange, xRange), Random.Range(-yRange, yRange));

        Vector2 newDestination = new Vector2(_child.transform.position.y + randomOffset.x, _child.transform.position.y + randomOffset.y);
        float clampX = newDestination.x;
        float clampY = newDestination.y;
        clampX = Mathf.Clamp(clampX, _aic.SceneBounds[3], _aic.SceneBounds[1]);
        clampY = Mathf.Clamp(clampY, _aic.SceneBounds[0], _aic.SceneBounds[2]);

        currDestination = new Vector3(clampX, clampY, 0f);
    }

    private void GetMousePos()
    {
        Vector3 getMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector2(getMouse.x, getMouse.y);
    }
}
