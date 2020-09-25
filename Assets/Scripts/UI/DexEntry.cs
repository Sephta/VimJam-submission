using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DexEntry : MonoBehaviour
{
    public CreatureData _entry = null;
    public Image _entryImage = null;
    public Text _entryText = null;
    [ReadOnly] public bool _discovered = false;

    [ReadOnly] public PlayerInventory _pi = null;

    void Awake()
    {
        if (_entry == null)
            Debug.Log("Warning. Entry data in " + gameObject.name + " is null.");
        if (_entryImage == null)
            _entryImage = GetComponent<Image>();

        if (GameObject.Find("PlayerInv") != null)
            _pi = GameObject.Find("PlayerInv").GetComponent<PlayerInventory>();
        else
            Debug.Log("Warning. Player Inv reference in " + gameObject.name + " is null.");
        
        if (_entryText != null)
            _entryText.text = "???";
    }

    void Update()
    {
        foreach (CreatureData c in _pi._dexList)
        {
            if (_entry == c)
            {
                _discovered = true;
                ChangeImage();
            }
        }
    }

    void ChangeImage()
    {
        _entryImage.sprite = _entry.CreatureImage;
        _entryImage.color = Color.white;
        _entryText.text = _entry.CreatureName;
    }
}
