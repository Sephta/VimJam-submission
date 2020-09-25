using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureCollisions : MonoBehaviour
{
    public AIController aic = null;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Creature")
        {
            if (aic != null)
                aic.colStatus = true;
        }
    }
}
