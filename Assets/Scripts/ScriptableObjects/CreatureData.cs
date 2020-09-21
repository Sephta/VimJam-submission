using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreatureDataAsset", menuName = "CreatureData Asset", order = 1)]
public class CreatureData : ScriptableObject
{
    // PRIVATE DATA
    [SerializeField] private string _creatureName = "";
    [SerializeField] private Sprite _creatureImage = null;


    // PUBLIC DATA
    public string CreatureName { get { return _creatureName; } }
    public Sprite CreatureImage { get { return _creatureImage; } }
}
