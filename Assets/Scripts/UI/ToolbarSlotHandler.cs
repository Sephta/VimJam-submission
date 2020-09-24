using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolbarSlotHandler : MonoBehaviour
{
    public PlayerInventory _pi = null;
    public int slotNum = 0;
    public Sprite _defaultSprite = null;

    void Start()
    {
        if (GameObject.Find("PlayerInv") != null)
            _pi = GameObject.Find("PlayerInv").GetComponent<PlayerInventory>();
    }

    void Update()
    {
        if (_pi != null)
            if (_pi._toolBar[slotNum - 1] == null)
                GetComponent<Image>().sprite = _defaultSprite;
    }
}
