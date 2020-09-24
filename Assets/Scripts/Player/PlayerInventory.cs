using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{
    [Header("Player Data")]
    public int _playerEXP = 0;
    public int _inventoryMax = 0;
    [ReadOnly] public int _currInventoryAmount = 0;
    [SerializeField] public List<CreatureData> _creatureType = new List<CreatureData>();
    [SerializeField] public List<int> _creatureAmount = new List<int>();

    [SerializeField, ReadOnly] public List<CreatureData> _toolBar = new List<CreatureData>(new CreatureData[3]);
    [SerializeField, ReadOnly] public List<CreatureData> _dexList = new List<CreatureData>();

    [Header("Instance Data")]
    public static PlayerInventory _instance;
    [ReadOnly] public bool inMenu = false;
    private bool gameStart = false;

    void Awake()
    {
        if (!gameStart)
        {
            _instance = this;
            SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
            gameStart = true;
        }
    }

    void Update()
    {
        foreach (CreatureData c in _creatureType)
        {
            if (!_dexList.Contains(c))
                _dexList.Add(c);
        }

        // Value should never be bellow zero        
        if (_playerEXP < 0)
            _playerEXP = 0;

        // if (GameObject.Find("PlayerMaster") != null)
        //     inMenu = true;
        // else
        //     inMenu = false;

        if (SceneManager.GetSceneAt(1).name == "Main")
        {
            inMenu = true;
        }
        else
            inMenu = false;
    }
}
