using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragDropCreature : MonoBehaviour
{
    // PUBLIC VARS
    public CreatureData _cData = null;
    public GameObject _cImage = null;
    public SpriteRenderer _renderer = null;
    public bool isHeld = false;

    // PRIVATE VARS
    private GameObject _imageRef = null;
    private Vector3 mousePos = Vector3.zero;


    // void Awake() {}
    // void Start() {}

    void Update()
    {
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
            _renderer.enabled = false;
            
            if (_cImage != null)
            {
                _imageRef = Instantiate(_cImage, new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity);
                Transform child = _imageRef.transform.GetChild(0);
                child.gameObject.GetComponent<Image>().sprite = _cData.CreatureImage;
            }

            if (_renderer != null)
            {
                _renderer.sortingLayerName = "UI";
                _renderer.sortingOrder = 1;
            }
        }
    }

    void OnMouseUp()
    {
        isHeld = false;
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
