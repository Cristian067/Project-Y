using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/Upgrade")]


public class UpgradeSO : ScriptableObject
{

    public enum Who
    {
        Player,
        Enemy,
        Both,

    }

    public Who whoToAdd;


    public enum UpgradeType
    {
        StatModification,
        Effect,
        Special

    }

    public UpgradeType type;

    public bool unique;


    public enum Alignment
    {
        Positive,
        Negative,

    }

    public Alignment alignment;
    public string description;
    
    public enum StatToModify
    {
        Damage,
        Speed,
        PickupRange,
        Health,

    }

    [Space(2)]
    [Header("If is type Stat Modification:")]

    public StatToModify[] modify;

    public float[] valuesToAdd;

    [Space(2)]
    [Header("If is type Special:")]
    public SpecialSO special;



    // public void Use(GameObject owner)
    // {

    //     //PlayerController player = owner.GetComponent<PlayerController>();

    
        
    // }


}
