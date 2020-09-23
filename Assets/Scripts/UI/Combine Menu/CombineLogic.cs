using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombineLogic : MonoBehaviour
{
    [Header("Slots")]
    public SlotData slotA = null;
    public SlotData slotB = null;
    public Sprite defaultSprite = null;

    [Header("Player + Creature Data")]
    public PlayerData _pData = null;
    public CreatureMaster _cMaster = null;
    [SerializeField] public List<GameObject> ToCombine = new List<GameObject>(new GameObject[2]);

    [Header("Combos")]
    public EvolutionList _evoList = null;
    public GameObject _newCreatureObject = null;

    void Awake()
    {
        if (GameObject.Find("PlayerMaster") != null)
            _pData = GameObject.Find("PlayerMaster").GetComponent<PlayerData>();

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

    // void Update() {}

    public void ClearSlots(bool killFlag = false)
    {
        if (slotA != null)
        {
            // Clear data from slotA
            if (defaultSprite != null)
                slotA.transform.GetChild(0).GetComponent<Image>().sprite = defaultSprite;
            else
                slotA.transform.GetChild(0).GetComponent<Image>().sprite = null;
            slotA._slotCreature = null;
            Debug.Log("Slot A Cleared.");
        }

        if (slotB != null)
        {
            // Clear data from slotB
            if (defaultSprite != null)
                slotB.transform.GetChild(0).GetComponent<Image>().sprite = defaultSprite;
            else
                slotB.transform.GetChild(0).GetComponent<Image>().sprite = null;

            slotB._slotCreature = null;
            Debug.Log("Slot B Cleared.");
        }

        if (!killFlag)
        {
            for (int i = 0; i < ToCombine.Count; i++)
            {
                if (ToCombine[i] != null)
                    _cMaster._creatures.Add(ToCombine[i]);
                ToCombine[i] = null;
            }
        }
        else
        {
            for (int i = 0; i < ToCombine.Count; i++)
            {
                Destroy(ToCombine[i]);
                ToCombine[i] = null;
            }
        }
    }

    public void CombineCreatures()
    {
        if (slotA != null && slotB != null)
        {
            CreatureData cdA = slotA._slotCreature.GetComponent<AIController>()._creatureData;
            CreatureData cdB = slotB._slotCreature.GetComponent<AIController>()._creatureData;
            foreach (Evolution evo in _evoList.EvoList)
            {
                if (evo.Creature01 == cdA && evo.Creature02 == cdB ||
                    evo.Creature01 == cdB && evo.Creature02 == cdA)
                {
                    ProduceNewCreature(evo.ResultCreature);
                    _pData.RemoveCreature(cdA);
                    _pData.RemoveCreature(cdB);
                    ClearSlots(true);
                }
            }

            ClearSlots(false);
        }
    }

    private void ProduceNewCreature(CreatureData newCreature)
    {
        // Add to player inventory
        _pData.AddCreature(newCreature);

        // Create new creature and spawn it inside of playpen
        GameObject refr = Instantiate(_newCreatureObject, Vector3.zero, Quaternion.identity, _cMaster.transform);

        AIController _aic = refr.GetComponent<AIController>();
        Transform _child = refr.transform.GetChild(0);
        DragDropCreature _ddc = _child.GetComponent<DragDropCreature>();

        _aic._creatureID = _cMaster.currCreatureID;
        _aic._creatureData = newCreature;
        _aic.SceneBounds = _cMaster.SceneBounds;
        _cMaster.currCreatureID++;

        _child.transform.position = _cMaster.FindRandomPos(_cMaster.SceneBounds);
        _child.GetComponent<SpriteRenderer>().sprite = newCreature.CreatureImage;

        _ddc._cData = newCreature;

        refr.name = "Creature - " + _aic._creatureID.ToString();
        _cMaster._creatures.Add(refr);
    }
}
