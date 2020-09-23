using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDataUpdater : MonoBehaviour
{
    [Header("Dependencies")]
    public PlayerInventory _pi = null;

    [Header("UI Data to Update")]
    public Text _expCounter = null;
    public GameObject _popupText = null;


    // void Awake() {}
    void Start()
    {
        if (GameObject.Find("PlayerInv") != null)
            _pi = GameObject.Find("PlayerInv").GetComponent<PlayerInventory>();
        if (_pi == null)
            Debug.Log("Warning. Reference to PlayerInventory from UIDataUpdater.cs is null.");
    }

    void Update()
    {
        if (_pi != null)
        {
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (_popupText != null && ("EXP " + _pi._playerEXP.ToString()) != _expCounter.text)
        {
            // GameObject refr = Instantiate(_popupText, Vector3.zero, Quaternion.identity, gameObject.transform);
            // refr.GetComponent<RectTransform>().position = new Vector3(-467f, -20f, 0f);
        }
        _expCounter.text = "EXP " + _pi._playerEXP.ToString();
    }
}
