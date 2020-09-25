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
    [SerializeField, ReadOnly] private PlayerData _pData = null;

    private GameObject _child = null;
    private AIController _aic = null;
    private Rigidbody2D _crb = null;

    [SerializeField, ReadOnly] private bool waitTime = false;
    [SerializeField, ReadOnly] private float _timeLeft = 0f;
    [SerializeField, ReadOnly] private int _time = 0;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_child == null)
           _child = animator.transform.GetChild(0).gameObject;
        if (_aic == null)
            _aic = animator.transform.gameObject.GetComponent<AIController>();
        if (_crb == null)
            _crb = _child.GetComponent<Rigidbody2D>();
        if (GameObject.Find("PlayerMaster") != null)
            _pData = GameObject.Find("PlayerMaster").GetComponent<PlayerData>();
        
        direction = CalculateDirection();
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
            if (_pData != null && _pData.isHolding && _pData._currItemData != null && _pData._currItemData.ItemType == ItemClass.iType.Food)
            {
                animator.SetBool("isHoldingFood", true);
            }
            animator.SetBool("mouseClose", true);
        }

        if (!waitTime)
        {
            waitTime = true;
            _time = Random.Range(0, 5);
            _timeLeft = _time;

            direction = CalculateDirection();
        }
        else
        {
            _timeLeft -= Time.deltaTime;
            _timeLeft = Mathf.Clamp(_timeLeft, 0, _time);

            if (_timeLeft == 0)
                waitTime = false;
        }

        _crb.AddForce(((direction * moveSpeed * -1f) - _crb.velocity) * Time.deltaTime, ForceMode2D.Impulse);

        if (Vector3.Distance(_child.transform.position, currDestination) < 2f || _aic.colStatus)
        {
            // FindNewDestination(animator);
            direction = CalculateDirection();
            animator.transform.gameObject.GetComponent<AIController>().colStatus = false;
        }
    }

    private Vector2 CalculateDirection(Vector2 destination)
    {
        float clampX = _child.transform.position.x - destination.x;
        float clampY = _child.transform.position.y - destination.y;

        clampX = Mathf.Clamp(clampX, -1f, 1f);
        clampY = Mathf.Clamp(clampY, -1f, 1f);

        Vector2 result = new Vector2(clampX, clampY);

        return result;
    }

    private Vector2 CalculateDirection()
    {
        float clampX = ((float)Random.Range(-100, 101)) / 100;
        float clampY = ((float)Random.Range(-100, 101)) / 100;

        clampX = Mathf.Clamp(clampX, -1f, 1f);
        clampY = Mathf.Clamp(clampY, -1f, 1f);

        Vector2 result = new Vector2(clampX, clampY);

        return result;
    }

    private void GetMousePos()
    {
        Vector3 getMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector2(getMouse.x, getMouse.y);
    }
}
