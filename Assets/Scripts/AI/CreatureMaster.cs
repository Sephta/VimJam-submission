using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMaster : MonoBehaviour
{
    // PUBLIC VARS
    [Header("Dependencies")]
    public GameObject _cContainer = null;

    [Header("Area Spawn Params")]
    public CM_Mode _type;
    [SerializeField] public List<GameObject> _creatures = new List<GameObject>();
    [SerializeField] public List<float> SceneBounds = new List<float>(new float[4]);

    [Header("Player Data")]
    public PlayerData _pData = null;

    public enum CM_Mode
    {
        main = 0,
        local = 1
    }

    // PRIVATE VARS
    private GameObject refr = null;
    private int currCreatureID = 0;


    void Awake()
    {
        if (GameObject.Find("PlayerMaster") != null)
            _pData = GameObject.Find("PlayerMaster").GetComponent<PlayerData>();
    }

    void Start()
    {
        switch(_type)
        {
            case CM_Mode.main:
                SpawnPlayerInventory();
                break;

            case CM_Mode.local:
                break;
        }
    }

    void Update()
    {
        CheckCreatureBounds();
    }

    // void FixedUpdate() {}

    private void SpawnPlayerInventory()
    {
        if (_cContainer == null)
            return;

        if (_pData != null)
        {
            foreach (CreatureData creature in _pData._creatureType)
            {
                for (int i = 0; i < _pData._creatureAmount[_pData._creatureType.IndexOf(creature)]; i++)
                {
                    Vector3 spawnPos = FindRandomPos(SceneBounds);
                    refr = Instantiate(_cContainer, Vector3.zero, Quaternion.identity, transform);

                    AIController _aic = refr.GetComponent<AIController>();
                    Transform _child = refr.transform.GetChild(0);
                    DragDropCreature _ddc = _child.GetComponent<DragDropCreature>();

                    _aic._creatureID = currCreatureID;
                    _aic._creatureData = creature;
                    _aic.SceneBounds = this.SceneBounds;
                    currCreatureID++;
                    _aic.SetCreatureImage();

                    _child.transform.position = spawnPos;
                    _child.GetComponent<SpriteRenderer>().sprite = creature.CreatureImage;

                    _ddc._cData = creature;

                    refr.name = "Creature - " + _aic._creatureID.ToString();
                    _creatures.Add(refr);
                }
            }
        }
    }

    private Vector3 FindRandomPos(List<float> bounds)
    {
        Vector3 result = Vector3.zero;

        result.x = Random.Range(bounds[3], bounds[1]);
        result.y = Random.Range(bounds[2], bounds[0]);

        return result;
    }

    private void CheckCreatureBounds()
    {
        foreach (GameObject creature in _creatures)
        {
            Transform child = creature.transform.GetChild(0);
            if (child.transform.position.x < SceneBounds[3] || child.transform.position.x > SceneBounds[1]
             || child.transform.position.y > SceneBounds[0] || child.transform.position.y < SceneBounds[2])
            {
                child.transform.position = FindRandomPos(SceneBounds);
            }
        }
    }
}
