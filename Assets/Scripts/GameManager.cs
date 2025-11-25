using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;







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



    [SerializeField] private PlayerController playerScript;

    [SerializeField] private int lives = 3;
    [SerializeField] private int maxLives = 6;
    [SerializeField] private float specialCharge;

    [SerializeField] private int pointsForUpgrade;
    [SerializeField] private int totalPoints;
    [SerializeField] private int points;

    [SerializeField] private GameObject barrier;
    [SerializeField] private bool barrierInRecharge;
    [SerializeField] private GameObject barrierRechargeParticles;
    [SerializeField] private float barrierCooldown;

    [Header("Base Stats")]
    [SerializeField] private float damageBase = 1;
    [SerializeField] private float speedBase = 8;

    [Header("Stats")] 
    [SerializeField] private float damage = 1;
    [SerializeField] private float speed = 8;


    [SerializeField] private List<UpgradeSO> upgrades;
    [SerializeField] private List<UpgradeSO> enemyUpgrades;

    [SerializeField] private UpgradeSO special;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;


        UIManager.instance.RefreshStatsUi();
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
        if (damage <= 0)
        {
            damage = 1;
        }
        if (speed <= 0)
        {
            speed = 0.1f;
        }

        if (upgrades.Contains(UpgradesManager.instance.effects.barrier) && !barrier.active && !barrierInRecharge)
        {
            barrier.SetActive(true);
            
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
        UIManager.instance.RefreshStatsUi();
        StartCoroutine(playerScript.HitInCooldown());
        if (lives == 0)
        {

            Debug.Log("GameOver");
            Lose();
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

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public UpgradeSO GetSpecial()
    {
        return special;
    }

    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        totalPoints += pointsToAdd;
        UIManager.instance.RefreshStatsUi();

        if (points >= pointsForUpgrade)
        {
            UpgradesManager.instance.DisplayUpgrades();
            points -= pointsForUpgrade;
        }
    }

    public void AdquireUpgrade(int side, UpgradeSO upgrade)
    {
        if (upgrade == null)
        {
            return;
        }

        if (upgrade.type == UpgradeSO.UpgradeType.Special)
        {
            if (upgrade != null)
            {
                special = upgrade;
            }
            
        }
        else
        {
            if (side == 0)
            {
                upgrades.Add(upgrade);

            }

            else
            {
                enemyUpgrades.Add(upgrade);

            }
        }

        ReloadStats();
    }

    public float GetPlayerDamage()
    {
        return damage;
    }

    public int GetPlayerLives()
    {
        return lives;
    }

    public int GetTotalPoints()
    {
        return totalPoints;
    }

    public int GetPoints()
    {
        return points;
    }

    public int GetPointsToUpgrade()
    {
        return pointsForUpgrade;
    }


    public UpgradeSO[] GetUpgrades()
    {
        return upgrades.ToArray();
    }

    

    public void DestroyBarrier()
    {

        barrier.SetActive(false);
        StartCoroutine(RechargeBarrier());
        
    }

    private IEnumerator RechargeBarrier()
    {
        barrierInRecharge =true;
        barrierRechargeParticles.SetActive(true);
        yield return new WaitForSeconds(barrierCooldown);
        barrierRechargeParticles.SetActive(false);
        barrier.SetActive(true);
        barrierInRecharge = false;

        
    }


    public void Win()
    {
        UIManager.instance.DisplayWinPanel();
        Pause();
    }
    public void Lose()
    {
        UIManager.instance.DisplayLosePanel();
        Pause();
    }



    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().path);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }


}
