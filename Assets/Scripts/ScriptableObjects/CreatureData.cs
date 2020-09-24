using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreatureDataAsset", menuName = "ScriptableObjects/CreatureData Asset", order = 1)]
public class CreatureData : ScriptableObject
{
    // PRIVATE DATA
    [SerializeField] private string _creatureName = "";
    [SerializeField] private int _creatureID = 0;
    [SerializeField] private int _soundIndexPickup = 0;
    [SerializeField] private int _soundIndexDrop = 0;
    [SerializeField] private Sprite _creatureImage = null;
    [SerializeField] private int _creatureEXP = 0;


    // PUBLIC DATA
    public string CreatureName { get { return _creatureName; } }
    public int CreatureID { get { return _creatureID; } }
    public int CreaturePickupSound { get { return _soundIndexPickup; } }
    public int CreatureDropSound { get { return _soundIndexDrop; } }
    public Sprite CreatureImage { get { return _creatureImage; } }
    public int CreatureEXP { get { return _creatureEXP; } }
}
