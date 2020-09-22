using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EvolutionListAsset", menuName = "ScriptableObjects/Evolution List Asset", order = 2)]
public class EvolutionList : ScriptableObject
{
    [SerializeField] public List<Evolution> EvoList = new List<Evolution>();
}
