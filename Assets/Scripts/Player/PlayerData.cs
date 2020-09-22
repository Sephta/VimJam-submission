using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // PUBLIC VARS
    [Header("Dependencies")]
    public GameObject _cContainer = null;

    [Header("Player Data")]
    // [SerializeField, ReadOnly] public List<GameObject> _inventory = new List<GameObject>();

    [SerializeField] public List<CreatureData> _creatureType = new List<CreatureData>();
    [SerializeField] public List<int> _creatureAmount = new List<int>();

    [Header("Drag and Drop Data")]
    [ReadOnly] public CreatureData _currCreatureData = null;
    [ReadOnly] public GameObject _currCreature = null;
    public bool isHolding = false;


    // PRIVATE VARS

    void Awake() 
    {
        if (_cContainer == null)
            Debug.Log("Warning. reference to CreatureContainer in PlayerData is null.");
    }

    void Start() {}

    void Update()
    {
        if (!isHolding)
        {
            _currCreatureData = null;
            _currCreature = null;
        }
    }

    // void FixedUpdate() {}

    public void AddCreature(CreatureData newCreature)
    {
        if (_creatureType.Contains(newCreature))
            _creatureAmount[_creatureType.IndexOf(newCreature)]++;
        else
            _creatureType.Add(newCreature);
    }

    public void RemoveCreature()
    {
        
    }
}
