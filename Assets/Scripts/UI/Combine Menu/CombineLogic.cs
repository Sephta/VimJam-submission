using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombineLogic : MonoBehaviour
{
    [Header("Slots")]
    public SlotData slotA = null;
    public SlotData slotB = null;

    [Header("Player + Creature Data")]
    public PlayerData _pData = null;
    public CreatureMaster _cMaster = null;

    void Awake()
    {
        if (slotA == null || slotB == null)
            Debug.Log("Warning. SlotData in CombineLogic is null or missing.");
    }

    void Start()
    {
        if (GameObject.Find("PlayerMaster") != null)
            _pData = GameObject.Find("PlayerMaster").GetComponent<PlayerData>();
        else
            Debug.Log("Warning (in CombineLogic.cs). PlayerData missing or null.");
        
        if (GameObject.Find("CreatureMaster") != null)
            _cMaster = GameObject.Find("CreatureMaster").GetComponent<CreatureMaster>();
        else
            Debug.Log("Warning (in CombineLogic.cs). CreatureMaster missing or null.");
    }

    public void ClearSlots()
    {
        if (slotA != null)
        {
            // Clear data from slotA
            slotA.transform.GetChild(0).GetComponent<Image>().sprite = null;
            slotA._cData = null;
            Debug.Log("Slot A Cleared.");
        }

        if (slotB != null)
        {
            // Clear data from slotB
            slotB.transform.GetChild(0).GetComponent<Image>().sprite = null;
            slotB._cData = null;
            Debug.Log("Slot B Cleared.");
        }
    }

    public void CombineCreatures()
    {
        if (slotA != null && slotB != null)
        {
            

            // Then clear slots
            ClearSlots();
        }
    }
}
