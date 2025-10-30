using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/Upgrade")]


public class UpgradeSO : ScriptableObject
{

    public enum UpgradeType
    {
        StatModification,
        Effect,
        Special

    }

    [SerializeField] public UpgradeType type;


    public enum Alignment
    {
        Positive,
        Negative,

    }

    [SerializeField] public Alignment alignment;

    [SerializeField] public string description;
    
    
    //[Header("Modificaion de stat")]


    public enum StatToModify
    {
        Damage,
        Speed,

    }

    [SerializeField] public StatToModify modify;

    [SerializeField] public float valueToAdd;





}
