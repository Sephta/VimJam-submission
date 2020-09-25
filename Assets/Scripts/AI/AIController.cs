using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public int _creatureID = 0;
    public CreatureData _creatureData = null;
    [SerializeField] public List<float> SceneBounds = new List<float>(new float[4]);
    public bool colStatus = false;
}
