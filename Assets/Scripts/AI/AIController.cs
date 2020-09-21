using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public CreatureData _creatureData = null;
    [SerializeField] public List<int> SceneBounds = new List<int>(new int[4]);
    public SpriteRenderer _renderer = null;
    public bool colStatus = false;

    void Awake()
    {
        if (_creatureData == null)
            Debug.Log("Warning. reference to '_creatureData' on" + gameObject.name + " is null.");
    }

    void Start()
    {
        if (_creatureData != null && _renderer != null)
            _renderer.sprite = _creatureData.CreatureImage;
    }
}
