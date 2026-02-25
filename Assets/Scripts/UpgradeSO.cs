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

    }

    [SerializeField] public Who whoToAdd;


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
        PickupRange,
        Health,

    }

    [Space(2)]
    [Header("If is type Stat Modification:")]

    [SerializeField] public StatToModify modify;

    [SerializeField] public float valueToAdd;

    [Space(2)]
    [Header("If is type Special:")]


    [SerializeField] public SpecialSO special;



    // public void Use(GameObject owner)
    // {

    //     //PlayerController player = owner.GetComponent<PlayerController>();

    
        
    // }


}
