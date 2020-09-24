using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragDropCreature : MonoBehaviour
{
    // PUBLIC VARS
    public PlayerData _pData = null;
    public CreatureData _cData = null;
    public GameObject _cImage = null;
    public SpriteRenderer _renderer = null;
    public bool isHeld = false;

    // PRIVATE VARS
    private GameObject _imageRef = null;
    private Vector3 mousePos = Vector3.zero;


    // void Awake() {}

    void Start()
    {
        if (GameObject.Find("PlayerMaster") != null)
            _pData = GameObject.Find("PlayerMaster").GetComponent<PlayerData>();
    }

    void Update()
    {
        if (_pData == null && GameObject.Find("PlayerMaster") != null)
            _pData = GameObject.Find("PlayerMaster").GetComponent<PlayerData>();

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

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
            _renderer.enabled = false;
            
            if (_cImage != null && _cData != null)
            {
                _imageRef = Instantiate(_cImage, new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity);
                _imageRef.name = "Held Creature";
                _imageRef.transform.SetParent(GameObject.Find("------------------- UI ------------------").transform);
                Transform child = _imageRef.transform.GetChild(0);
                child.gameObject.GetComponent<Image>().sprite = _cData.CreatureImage;
            }

            _pData._currCreature = transform.parent.gameObject;
            _pData._currCreatureData = _cData;

            if (_renderer != null)
            {
                _renderer.sortingLayerName = "UI";
                _renderer.sortingOrder = 1;
            }

            if (_pData._currCreatureData != null)
                AudioManager._instance.PlaySound(_pData._currCreatureData.CreaturePickupSound);
            else
                Debug.Log("wat?");
        }
    }

    void OnMouseUp()
    {
        // if (_pData._currCreatureData != null)
        //     AudioManager._instance.PlaySound(_pData._currCreatureData.CreatureDropSound);

        isHeld = false;
        // _pData.isHolding = false;
        _renderer.enabled = true;

        if (_imageRef != null)
        {
            Destroy(_imageRef);
            _imageRef = null;
        }

        if (_renderer != null)
        {
            _renderer.sortingLayerName = "Creature";
            _renderer.sortingOrder = 0;
        }
    }

    // void FixedUpdate() {}
}
