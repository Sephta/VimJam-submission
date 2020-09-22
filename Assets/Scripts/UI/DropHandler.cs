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
        }
    }
}
