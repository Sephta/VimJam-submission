using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClass
{
    public enum iType
    {
        Default = 0,
        Food    = 1,
        Toy     = 2,
    }
}

[CreateAssetMenu(fileName = "ShopItemAsset", menuName = "ScriptableObjects/ShopItem", order = 4), SerializeField]
public class ShopItemData : ScriptableObject
{
    public Sprite ItemImage = null;
    public ItemClass.iType ItemType = (ItemClass.iType)0;
    public string ItemName = "";
    public int ItemCost = 0;
}
