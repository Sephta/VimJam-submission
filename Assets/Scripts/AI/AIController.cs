﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    // Dependecies
    public Rigidbody2D _rb;
    public Animator _animator;

    public bool colStatus = false;

    // void Start() {}
    // void Update() {}
    // void FixedUpdate() {}

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Creature")
        {
            colStatus = true;
        }
    }
}
