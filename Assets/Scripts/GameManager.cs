using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerStats 
{
    public float damage;
    public float speed;
    public float pickupRange;
}




public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; }

    [SerializeField]private int levelNumber;

    [SerializeField] private PlayerController playerScript;

    [SerializeField] private int lives = 3;
    [SerializeField] public int maxLives { get; private set; } = 6;
    [SerializeField] public float specials = 2;
    [SerializeField] public int maxSpecials { get; private set; } = 5;

    [SerializeField] private float specialCharge;

    [SerializeField] private int pointsForUpgrade;
    [SerializeField] private int totalPoints;
    [SerializeField] private int points;

    [SerializeField] private GameObject barrier;
    [SerializeField] private bool barrierInRecharge;
    [SerializeField] private GameObject barrierRechargeParticles;
    [SerializeField] private float barrierCooldown;



    public PlayerStats baseStats;
    public PlayerStats modStats;
    public PlayerStats finalStats { get; private set;} = new PlayerStats();

    private float timeScaleSaved;

    public bool paused { get; private set; }
    public bool specialInCooldown { get; private set; }
    public float specialCooldown { get; private set; }


    public int enemies = 0;

    //private Data data = new Data();

    private string pathUserData = "save/UserData.json";

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

        ReloadStats();
    
        try
        {
            playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        }
        catch
        {
            Debug.LogWarning("No existe player");
        }

        Log.AddToLog($"Level {levelNumber} started");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause") && !paused )
        {
            Pause(true);
        }
    }

    [ContextMenu("Reload Stats")]
    public void ReloadStats()
    {

        modStats.speed = 0;
        modStats.damage = 0;
        modStats.pickupRange = 0;

        if (UpgradesManager.instance.playerUpgrades.Contains(UpgradesManager.instance.effects.barrier) && !barrierInRecharge)
        {
            barrier.SetActive(true);
        }

        foreach (UpgradeSO upgrade in UpgradesManager.instance.playerUpgrades)
        {
            
            if (upgrade.type == UpgradeSO.UpgradeType.StatModification)
            {
                for (int i = 0; i < upgrade.modify.Length;i++)
                {
                    if (upgrade.modify[i] == UpgradeSO.StatToModify.Damage)
                    {
                        modStats.damage += upgrade.valuesToAdd[i];
                    }
                    else if (upgrade.modify[i] == UpgradeSO.StatToModify.Speed)
                    {
                        modStats.speed += upgrade.valuesToAdd[i];
                    }
                    else if (upgrade.modify[i] == UpgradeSO.StatToModify.PickupRange)
                    {
                        modStats.pickupRange += upgrade.valuesToAdd[i];
                    }
                }
                
            }
        }

        finalStats.damage = baseStats.damage + modStats.damage;
        finalStats.speed = baseStats.speed + modStats.speed;
        finalStats.pickupRange = baseStats.pickupRange + modStats.pickupRange;

        if (finalStats.damage <= 0)
        {
            finalStats.damage = 1;
        }
        if (finalStats.speed <= 0)
        {
            finalStats.speed = 0.1f;
        }
        try
        {
            GameObject.Find("Magnet").GetComponent<Magnet>().ChangeMagnetRange(finalStats.pickupRange);
        }
        catch
        {
            Debug.Log("No se encuentra el magnet");
        }

        if (UpgradesManager.instance.playerUpgrades.Contains(UpgradesManager.instance.effects.barrier) && !barrier.active && !barrierInRecharge)
        {
            barrier.SetActive(true);
        }
        else if (UpgradesManager.instance.playerUpgrades.Contains(UpgradesManager.instance.effects.barrier) && barrierInRecharge)
        {
            barrierInRecharge = false;
        }

        UIManager.instance.RefreshStatsUi();
        Log.AddToLog("Stats reloaded");

    }

    public void Pause(bool usePanel = false)
    {
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
        paused = true;
        timeScaleSaved = Time.timeScale;
        //Time.timeScale = 0;
        if (usePanel)
        {
            UIManager.instance.DisplayPauseMenu();
        }
    }

    public void Unpause(bool useSaved = false)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (useSaved)
        {
            Time.timeScale = timeScaleSaved;
        }
        else
        {
            Time.timeScale = 1;
        }
        paused = false;
    }

    public void LoseLife()
    {
        lives--;
        UIManager.instance.RefreshStatsUi();
        StartCoroutine(playerScript.HitInCooldown());
        if (lives == 0)
        {
            Debug.Log("GameOver");
            Log.AddToLog("The player died");
            Lose();
        }
    }

    public void Heal(int lifes)
    {
        if (lives >= maxLives)
        {
            return;
        }
        lives += lifes;
        UIManager.instance.RefreshStatsUi();
    }

    public void SetHealth(int health)
    {
        lives = health;
        Log.AddToLog("Set player health to " + health);
        UIManager.instance.RefreshStatsUi();
    }

    public float GetSpeed()
    {
        return finalStats.speed;
    }

    public void SetSpeed(float newSpeed)
    {
        finalStats.speed = newSpeed;
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

    public void RechargeSpecial(float recharge)
    {
        if (specials >= maxSpecials)
        {
            return;
        }
        else
        {
            specials += recharge;
            UIManager.instance.RefreshStatsUi();
        }
    }

    public float GetPlayerDamage()
    {
        return finalStats.damage;
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


    

    [ContextMenu("Win Game")]
    public void Win()
    {

        Data data= new Data();
        string json = File.ReadAllText(pathUserData);
        data = JsonUtility.FromJson<Data>(json);

        data = RegistryUpgrades(data);

        if (!data.levelsCompleted[levelNumber])
        {
            data.levelsCompleted[levelNumber] = true;
        }
        
        if(data.levelsHighScore[levelNumber] < totalPoints)
        {
            data.levelsHighScore[levelNumber] = totalPoints;
        }


        StartCoroutine(ApiCalls.PostScore(pathUserData,levelNumber,totalPoints));

        json = JsonUtility.ToJson(data,true);
        File.WriteAllText(pathUserData, json);
        Log.AddToLog($"Game saved in: {Application.dataPath}/{pathUserData}");
        Log.AddToLog("The player won the level " + levelNumber);
        //Log.AddToLog("The player k " + levelNumber);
        UIManager.instance.DisplayWinPanel();
        Pause();

    }

    private Data RegistryUpgrades(Data data)
    {
        //Data data = new Data();
        // string json = File.ReadAllText(pathUserData);
        // data = JsonUtility.FromJson<Data>(json);

        foreach (var upgrade in UpgradesManager.instance.playerUpgrades)
        {
            if (!data.discoveredUpgrades.Contains(upgrade.name))
            {
                Log.AddToLog($"New upgrade registred: {upgrade.name}");
                data.discoveredUpgrades.Add(upgrade.name);
            }
        }
        return data;
        
    }
    public void Lose()
    {
        Log.AddToLog("The player lost the level " + levelNumber);
        UIManager.instance.DisplayLosePanel();
        Pause();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().path);
    }

    public void GoToMainMenu()
    {   
        //Log.AddToLog($"The player quit the level");
        SceneManager.LoadScene(0);
    }

    public bool isBarrierActive()
    {
        if (barrier.active && !barrierInRecharge)
        {
            return true;
        }
        else
        {
            return false;
        }
            
    }

    public void SetSpecialCooldown(float time)
    {
        specialInCooldown = true;
        specialCooldown = time;
        StartCoroutine(SpecialCooldown());
    }

    public IEnumerator SpecialCooldown()
    {
        float time = 0;

        while (time < specialCooldown)
        {
            time += Time.deltaTime;
            //Debug.Log(time);
            yield return null;
        }
        specialInCooldown = false;
    }


    void OnApplicationQuit()
    {
        //Log.AddToLog("User");
        Log.ExportLog();
    }
    

}
