using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // PUBLIC VARS
    [Header("Dependencies")]
    public GameObject _cContainer = null;

    [Header("Player Inv")]
    public PlayerInventory _pi = null;

    [Header("Drag and Drop Data")]
    [ReadOnly] public CreatureData _currCreatureData = null;
    [ReadOnly] public GameObject _currCreature = null;
    [ReadOnly] public ShopItemData _currItemData = null;
    [ReadOnly] public GameObject _currItem = null;
    public bool isHolding = false;


    // PRIVATE VARS


    void Awake() 
    {

        if (GameObject.Find("PlayerInv") != null)
        {
            _pi = GameObject.Find("PlayerInv").GetComponent<PlayerInventory>();
        }

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
        CheckInv();
    }

    // void FixedUpdate() {}

    public void AddCreature(CreatureData newCreature)
    {
        if (_pi._creatureType.Contains(newCreature))
            _pi._creatureAmount[_pi._creatureType.IndexOf(newCreature)]++;
        else
        {
            _pi._creatureType.Add(newCreature);
            _pi._creatureAmount.Add(1);
        }
    }

    public void RemoveCreature(CreatureData toRemove)
    {
        if (_pi._creatureType.Contains(toRemove))
        {
            _pi._creatureAmount[_pi._creatureType.IndexOf(toRemove)]--;
        }
        else
        {
            Debug.Log("Creature not contined in player inv. ??? Idk if this is possible or not");
            return;
        }
    }

    private void CheckInv()
    {
        for (int i = 0; i < _pi._creatureAmount.Count; i++)
        {
            if (_pi._creatureAmount[i] == 0)
            {
                _pi._creatureAmount.RemoveAt(i);
                _pi._creatureType.RemoveAt(i);
            }
        }
    }
}
