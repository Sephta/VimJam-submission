using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemDataContainer : MonoBehaviour
{
    [Header("Item Data")]
    public ShopItemData _item = null;

    [Header("Dependecies")]
    public Image _itemImage = null;
    public Text _itemName = null;
    public Text _itemCost = null;


    void Awake()
    {
        if (_item != null)
            PopulateShopItemFields();
    }

    private void PopulateShopItemFields()
    {
        _itemImage.sprite = _item.ItemImage;
        _itemName.text = _item.ItemName;
        _itemCost.text = "EXP " + _item.ItemCost.ToString();
    }
}
