using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    public Rigidbody2D _rb = null;
    public float speed = 0f;
    public Vector2 directionVector
    {
        get
        {
            Vector2 result = Vector2.zero;

            result.x = Input.GetAxis("Horizontal");
            result.y = Input.GetAxis("Vertical");

            return result;
        }
    }

    void Start() {}
    void Update()
    {
        if (_rb != null)
        {
            _rb.AddForce(directionVector * speed * Time.deltaTime, ForceMode2D.Force);
        }
    }
}
