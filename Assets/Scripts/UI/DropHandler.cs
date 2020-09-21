using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour
{
    public GraphicRaycaster _raycaster = null;

    EventSystem es = null;

    void Start()
    {
        if (GetComponent<GraphicRaycaster>() != null)
            _raycaster = GetComponent<GraphicRaycaster>();
        if (es == null)
            es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
            DetectDrop();
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
                    sd._cData = _pData._currCreature;
                    result.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = _pData._currCreatureData.CreatureImage;
                }
                _pData._currCreature = null;
                _pData._currCreatureData = null;
            }
        }
    }
}
