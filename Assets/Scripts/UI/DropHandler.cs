using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour
{
    [Header("Dependencies")]
    public GraphicRaycaster _raycaster = null;
    public CombineLogic _cl = null;
    public PlayerData _pData = null;
    public CreatureMaster _cMaster = null;
    public GameObject _itemEntity = null;

    EventSystem es = null;


    // void Awake() {}

    void Start()
    {
        if (GetComponent<GraphicRaycaster>() != null)
            _raycaster = GetComponent<GraphicRaycaster>();
        if (es == null)
            es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        if (GameObject.Find("PlayerMaster") != null)
            _pData = GameObject.Find("PlayerMaster").GetComponent<PlayerData>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
            DetectDrop();

        if (_pData == null && GameObject.Find("PlayerMaster") != null)
            _pData = GameObject.Find("PlayerMaster").GetComponent<PlayerData>();
    }

    void DetectDrop()
    {
        PointerEventData eventData = new PointerEventData(es);

        eventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        _raycaster.Raycast(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.name == "Slot A" || result.gameObject.name == "Slot B")
            {
                PlayerData _pData = GameObject.Find("PlayerMaster").GetComponent<PlayerData>();
                if (_pData._currCreature != null && _pData._currCreatureData != null)
                {
                    SlotData sd = result.gameObject.GetComponent<SlotData>();
                    if (sd._slotCreature == null)
                    {
                        sd._slotCreature = _pData._currCreature;
                        if (result.gameObject.name == "Slot A")
                            _cl.ToCombine[0] = _pData._currCreature;
                        else
                            _cl.ToCombine[1] = _pData._currCreature;
                        
                        _pData._currCreature.transform.GetChild(0).position = new Vector3(10000f, 0f, 0f);
                        _cMaster._creatures.Remove(_pData._currCreature);
                    }
                    result.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = _pData._currCreatureData.CreatureImage;
                }
                _pData._currCreature = null;
                _pData._currCreatureData = null;
            }

            if (result.gameObject.name == "Shop Item")
                SpawnItemInHand(result.gameObject);

            if (result.gameObject.name == "Free Menu")
                FreeCreature();
            
            if (result.gameObject.name == "Toolbar Slot 01")
                if (_pData._pi != null)
                    AddCreatureToToolbar(1, result.gameObject);

            if (result.gameObject.name == "Toolbar Slot 02")
                if (_pData._pi != null)
                    AddCreatureToToolbar(2, result.gameObject);

            if (result.gameObject.name == "Toolbar Slot 03")
                if (_pData._pi != null)
                    AddCreatureToToolbar(3, result.gameObject);
        }
    }

    private void SpawnItemInHand(GameObject itemSlot)
    {
        ShopItemData shopItem = itemSlot.GetComponent<ShopItemDataContainer>()._item;
        if (shopItem != null && _itemEntity != null)
        {
            Debug.Log("Item Grabbed: " + shopItem.ItemName);
            GameObject refr = Instantiate(_itemEntity, new Vector3(-15.5f, -0.61f , 0f), Quaternion.identity, _pData.transform);
            refr.GetComponent<ItemEntityData>()._itemData = shopItem;
        }
    }

    private void FreeCreature()
    {
        PlayerData _pData = GameObject.Find("PlayerMaster").GetComponent<PlayerData>();

        if (_pData != null)
        {
            if (_pData._currCreature != null && _pData._currCreatureData != null)
            {
                _pData.RemoveCreature(_pData._currCreatureData);
                _cMaster._creatures.Remove(_pData._currCreature);
                _pData._pi._playerEXP += _pData._currCreatureData.CreatureEXP;
                Destroy(_pData._currCreature);
                _pData.isHolding = false;
                _pData._pi._currInventoryAmount -= 1;
                _pData._pi._currInventoryAmount = Mathf.Clamp(_pData._pi._currInventoryAmount, 0, _pData._pi._inventoryMax);
            }
        }
    }

    private void AddCreatureToToolbar(int slotNum, GameObject slot)
    {
        Debug.Log("Maybe working...");
        Debug.Log(_pData._currCreatureData == null);
        if (_pData._currCreatureData != null && !_pData._pi.inMenu)
        {
            foreach (CreatureData cd in _pData._pi._toolBar)
            {
                if (_pData._currCreatureData == cd)
                {
                    _pData.isHolding = false;
                    return;
                }
            }
            Debug.Log("adding creature to toolbar");
            _pData._pi._toolBar[slotNum - 1] = _pData._currCreatureData;
            slot.GetComponent<Image>().sprite = _pData._currCreatureData.CreatureImage;
            _pData.isHolding = false;
        }
    }
}
