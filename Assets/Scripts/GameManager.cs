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

    public static GameManager instance { get; private set; }

    [SerializeField] private int lives = 3;
    [SerializeField] private int maxLives = 6;
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
    [SerializeField] private List<UpgradeSO> enemyUpgrades;




    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
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
            if (upgrade.type == UpgradeSO.UpgradeType.StatModification)
            {
                if (upgrade.modify == UpgradeSO.StatToModify.Damage)
                {
                    damage += upgrade.valueToAdd.ConvertTo<int>();
                }
                else if (upgrade.modify == UpgradeSO.StatToModify.Speed)
                {
                    speed += upgrade.valueToAdd;
                }
            }
        }

        if (speed <= 0)
        {
            speed = 0.1f;
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        Time.timeScale = 1;
    }


    public void LoseLife()
    {
        lives--;
        if (lives == 0)
        {
            Debug.Log("GameOver");
        }
    }

    public void Heal(int lifes)
    {
        lives += lifes;
    }


    public float GetSpeed()
    {
        return speed;
    }

    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;

        if (points >= pointsForUpgrade)
        {
            UpgradesManager.instance.DisplayUpgrades();
            points -= pointsForUpgrade;
        }
    }

    public void AdquireUpgrade(int side, UpgradeSO upgrade)
    {
        if (side == 0)
        {
            upgrades.Add(upgrade);

        }

        else
        {
            enemyUpgrades.Add(upgrade);

        }

        ReloadStats();
    }

    public int GetPlayerDamage()
    {
        return damage;
    }


}
