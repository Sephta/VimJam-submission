using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{
    [Header("Player Data")]
    public int _playerEXP = 0;
    [SerializeField] public List<CreatureData> _creatureType = new List<CreatureData>();
    [SerializeField] public List<int> _creatureAmount = new List<int>();

    [Header("Instance Data")]
    public static PlayerInventory _instance;
    private bool gameStart = false;

    void Awake()
    {
        if (!gameStart)
        {
            _instance = this;
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            gameStart = true;
        }
    }
}
