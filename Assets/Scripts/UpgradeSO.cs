using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/Upgrade")]


public class UpgradeSO : ScriptableObject
{

    public enum UpgradeType
    {
        StatModification,

    }

    [SerializeField] public UpgradeType type;
    
    
    //[Header("Modificaion de stat")]


    public enum StatToModify
    {
        Damage,
        Speed,

    }

    [SerializeField] public StatToModify modify;

    [SerializeField] public float valueToAdd;





}
