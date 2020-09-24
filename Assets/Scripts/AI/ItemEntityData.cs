using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ItemEntityData : MonoBehaviour
{
    public PlayerData _pData = null;
    public ShopItemData _itemData = null;
    public SpriteRenderer _spriteRenderer = null;

    [SerializeField] private bool isHeld = false;
    [SerializeField] private Vector3 mousePos = Vector3.zero;

    void Awake()
    {
        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (GameObject.Find("PlayerMaster") != null)
            _pData = GameObject.Find("PlayerMaster").GetComponent<PlayerData>();
        else
            Debug.Log("Warning <ItemEntityData>. PlayerData could not be found.");
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Set item image
        if (_itemData != null)
            _spriteRenderer.sprite = _itemData.ItemImage;
        
        // if currently being held, then follow mouse pos
        if (isHeld)
        {
            transform.position = new Vector2(mousePos.x, mousePos.y);
        }
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isHeld = true;
            _pData.isHolding = true;
            _pData._currItemData = _itemData;
            _pData._currItem = gameObject;
        }
    }

    void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isHeld = false;
            _pData.isHolding = false;
        }
    }
}
