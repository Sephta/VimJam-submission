using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EvolutionAsset", menuName = "ScriptableObjects/Evolution Asset", order = 3), SerializeField]
public class Evolution : ScriptableObject
{
    public CreatureData Creature01 = null;
    public CreatureData Creature02 = null;

    public CreatureData ResultCreature = null;
}
