using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class UpgradeTypes
{
    public enum Types
    {
        StatModification,
    }
}

public class GameManager : MonoBehaviour
{

    [SerializeField] private int lives = 3;
    [SerializeField] private float specialCharge;

    [SerializeField] private int pointsForUpgrade;
    [SerializeField] private int points;

    [Header("Base Stats")]
    [SerializeField] private int damageBase = 1;
    [SerializeField] private float speedBase = 8;

    [Header("Stats")]
    [SerializeField] private int damage = 1;
    [SerializeField] private float speed = 8;


    [SerializeField] private List<UpgradeSO> upgrades;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Reload Stats")]
    public void ReloadStats()
    {

        damage = damageBase;
        speed = speedBase;


        foreach (UpgradeSO upgrade in upgrades)
        {
            if(upgrade.type == UpgradeSO.UpgradeType.StatModification)
            {
                if (upgrade.modify == UpgradeSO.StatToModify.Damage)
                {
                    damage += upgrade.valueToAdd.ConvertTo<int>();
                }
                else if (upgrade.modify == UpgradeSO.StatToModify.Speed)
                {
                    speed += upgrade.valueToAdd.ConvertTo<int>();
                }
            }
        }
    }


    public void LoseLife()
    {
        lives--;
        if (lives == 0)
        {
            Debug.Log("GameOver");
        }
    }


    public float GetSpeed()
    {
        return speed;
    }

}
