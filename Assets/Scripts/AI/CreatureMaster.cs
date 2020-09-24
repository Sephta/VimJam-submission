using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMaster : MonoBehaviour
{
    // PUBLIC VARS
    [Header("Dependencies")]
    public GameObject _cContainer = null;

    [Header("Main Menu Spawn Params")]
    public CM_Mode _type;
    [SerializeField] public List<GameObject> _creatures = new List<GameObject>();
    [SerializeField] public List<float> SceneBounds = new List<float>(new float[4]);
    [ReadOnly] public int currCreatureID = 0;

    [Header("Local Spawn Params")]
    public int spawnLimit = 0;
    [SerializeField] public List<int> weightedTable = new List<int>();
    [SerializeField] public List<RarityTier> tiers = new List<RarityTier>();

    [Header("Spawn Lists")]
    [SerializeField] public List<CreatureData> commonSpawns = new List<CreatureData>();
    [SerializeField] public List<CreatureData> uncommonSpawns = new List<CreatureData>();
    [SerializeField] public List<CreatureData> rareSpawns = new List<CreatureData>();

    [Header("Player Data")]
    public PlayerData _pData = null;
    public PlayerInventory _pi = null;

    public enum CM_Mode
    {
        main = 0,
        local = 1
    }

    public enum RarityTier
    {
        common = 0,
        uncommon = 1,
        rare = 2
    }

    // PRIVATE VARS
    private GameObject refr = null;


    void Awake()
    {
        if (GameObject.Find("PlayerMaster") != null)
            _pData = GameObject.Find("PlayerMaster").GetComponent<PlayerData>();
    }

    void Start()
    {
        if (GameObject.Find("PlayerInv") != null)
            _pi = GameObject.Find("PlayerInv").GetComponent<PlayerInventory>();

        switch(_type)
        {
            case CM_Mode.main:
                if (_pData._pi != null)
                    SpawnPlayerInventory();
                break;

            case CM_Mode.local:
                CalculateAreaSpawns();
                break;
        }
    }

    void Update()
    {
        CheckCreatureBounds();
        if (_pi.inMenu)
            CheckToolBar();
    }

    private void CalculateAreaSpawns()
    {
        // Debug.Log("Creatures should be spawning...");

        for (int i = 0; i < spawnLimit; i++)
        {
            switch (WeightedProb(tiers, weightedTable))
            {
                case (int)RarityTier.common:
                    SpawnCreature(commonSpawns[Random.Range(0, commonSpawns.Count)]);
                    break;
                case (int)RarityTier.uncommon:
                    SpawnCreature(uncommonSpawns[Random.Range(0, uncommonSpawns.Count)]);
                    break;
                case (int)RarityTier.rare:
                    SpawnCreature(rareSpawns[Random.Range(0, rareSpawns.Count)]);
                    break;
            }
        }
    }

    /* https://www.youtube.com/watch?v=Nu-HEbb_z54&t=283s
     * Code written bellow heavily inspired by the above link to
     * Wintermute Digital's Youtube channel
    */
    private int WeightedProb(List<RarityTier> tiers, List<int> weights)
    {
        int result = 0;

        float totalWeight = 0;
        foreach (int w in weights)
            totalWeight += w;
        
        float p = Random.Range(0, totalWeight);

        float runningTotal = 0;
        for (int i = 0; i < weights.Count; i++)
        {
            runningTotal += weights[i];
            if (runningTotal > p)
                return (int)tiers[i];
        }

        return result;
    }

    private void SpawnCreature(CreatureData newCreature)
    {
        refr = Instantiate(_cContainer, Vector3.zero, Quaternion.identity, transform);

        AIController _aic = refr.GetComponent<AIController>();
        Transform _child = refr.transform.GetChild(0);
        DragDropCreature _ddc = _child.GetComponent<DragDropCreature>();

        _aic._creatureID = currCreatureID;
        _aic._creatureData = newCreature;
        _aic.SceneBounds = this.SceneBounds;
        currCreatureID++;

        _child.transform.position = FindRandomPos(SceneBounds);
        _child.GetComponent<SpriteRenderer>().sprite = newCreature.CreatureImage;

        _ddc._cData = newCreature;

        refr.name = _aic._creatureData.CreatureName + " - " + _aic._creatureID.ToString();
        _creatures.Add(refr);
        // Debug.Log("Spawned: " + newCreature.CreatureName + " - " + _aic._creatureID);
    }

    private void SpawnPlayerInventory()
    {
        if (_cContainer == null)
            return;

        if (_pData != null)
        {
            foreach (CreatureData creature in _pData._pi._creatureType)
            {
                for (int i = 0; i < _pData._pi._creatureAmount[_pData._pi._creatureType.IndexOf(creature)]; i++)
                {
                    refr = Instantiate(_cContainer, Vector3.zero, Quaternion.identity, transform);

                    AIController _aic = refr.GetComponent<AIController>();
                    Transform _child = refr.transform.GetChild(0);
                    DragDropCreature _ddc = _child.GetComponent<DragDropCreature>();

                    _aic._creatureID = currCreatureID;
                    _aic._creatureData = creature;
                    _aic.SceneBounds = this.SceneBounds;
                    currCreatureID++;

                    _child.transform.position = FindRandomPos(SceneBounds);
                    _child.GetComponent<SpriteRenderer>().sprite = creature.CreatureImage;

                    _ddc._cData = creature;

                    refr.name = _aic._creatureData.CreatureName + " - " + _aic._creatureID.ToString();
                    _creatures.Add(refr);
                }
            }
        }
    }

    public Vector3 FindRandomPos(List<float> bounds)
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

    private void CheckToolBar()
    {
        foreach (CreatureData cd in _pData._pi._toolBar)
        {
            if (cd != null)
            {
                _pData.AddCreature(cd);
                SpawnCreature(cd);
                _pData._pi._toolBar[_pData._pi._toolBar.IndexOf(cd)] = null;
            }
        }
    }
}
