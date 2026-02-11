using System;
using System.Collections;

using System.IO;
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

[Serializable]
public class ApiData
{

    public string api_token = "ZHVxZUtGF4E0wzz0400BRy8imjHDgZPmL5m5UD5VYBUCstloOUH2sSbbS9ef";
    public string name;
    public string email;
    public int puntuacion;
   
}





// public class UpgradeTypes
// {
//     public enum Types
//     {
//         StatModification,
//     }
// }

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
    private PlayerStats finalStats = new PlayerStats();

    // [Header("Base Stats")]
    // [SerializeField] private float damageBase = 1;
    // [SerializeField] private float speedBase = 8;

    // [Header("Stats")] 
    // [SerializeField] private float damage = 1;
    // [SerializeField] private float speed = 8;


    // [SerializeField] private List<UpgradeSO> upgrades;
    // [SerializeField] private List<UpgradeSO> enemyUpgrades;


        public string bearerToken;


    private float timeScaleSaved;

    public bool paused { get; private set; }
    public bool specialInCooldown { get; private set; }
    public float specialCooldown { get; private set; }


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

        //StartCoroutine(GetFromApi("https://phpstack-1076337-5399863.cloudwaysapps.com/api/classification/ZHVxZUtGF4E0wzz0400BRy8imjHDgZPmL5m5UD5VYBUCstloOUH2sSbbS9ef"));
        Time.timeScale = 1;
        //Win();

        UIManager.instance.RefreshStatsUi();
        ReloadStats();

        try
        {
            playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        }
        catch
        {
            Debug.LogWarning("No existe player");
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Pause") && !paused )//Input.GetButtonDown("Pause") && !DialoguesManager.instance.IsOnDialogue() ||Input.GetButtonDown("Pause") && !paused)
        {
            Pause(true);
        }

    }

    [ContextMenu("Reload Stats")]
    public void ReloadStats()
    {

        // finalStats.damage = baseStats.damage;
        // finalStats.speed = baseStats.speed;


        if (UpgradesManager.instance.upgrades.Contains(UpgradesManager.instance.effects.barrier) && !barrierInRecharge)
        {
            barrier.SetActive(true);
        }

        foreach (UpgradeSO upgrade in UpgradesManager.instance.upgrades)
        {
            if (upgrade.type == UpgradeSO.UpgradeType.StatModification)
            {
                if (upgrade.modify == UpgradeSO.StatToModify.Damage)
                {
                    modStats.damage += upgrade.valueToAdd;
                }
                else if (upgrade.modify == UpgradeSO.StatToModify.Speed)
                {
                    modStats.speed += upgrade.valueToAdd;
                }
                else if (upgrade.modify == UpgradeSO.StatToModify.PickupRange)
                {
                    modStats.pickupRange += upgrade.valueToAdd;
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
        


        if (UpgradesManager.instance.upgrades.Contains(UpgradesManager.instance.effects.barrier) && !barrier.active && !barrierInRecharge)
        {
            barrier.SetActive(true);
        }
        else if (UpgradesManager.instance.upgrades.Contains(UpgradesManager.instance.effects.barrier) && barrierInRecharge)
        {
            barrierInRecharge = false;
        }

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

    //public UpgradeSO GetSpecial()
    //{
    //    return special;
    //}

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

        //baseStats.specialC += recharge.ConvertTo<int>();
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


    //public List<UpgradeSO> GetUpgrades()
    //{
    //    return upgrades;
    //}
    //public List<UpgradeSO> GetEnemyUpgrades()
    //{
    //    return enemyUpgrades;
    //}
    

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

    private IEnumerator GetFromApi(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Respuesta: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }


    

    private IEnumerator PostAPi()
    {

        Data data = new Data();
        string json = File.ReadAllText(pathUserData);
        data = JsonUtility.FromJson<Data>(json);


        ApiData postData= new ApiData();
        postData.name = levelNumber+"_"+data.username;
        //postData.email = data.email;
        postData.puntuacion = totalPoints;
        string jsonHS = JsonUtility.ToJson(postData);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonHS);


        // using(var webRequest = new UnityWebRequest("g", "POST"))
        // {
        //     webRequest.uploadHandler = new UploadHandlerRaw(bytes);
        //     webRequest.downloadHandler = new DownloadHandlerBuffer();
        //     webRequest.certificateHandler = new ForceAcceptAllCertificates();
        //     webRequest.SetRequestHeader("accept", "application/json");
        //     webRequest.SetRequestHeader("Content-Type", "application/json");

        //     // Btw afaik you can simply
        //     await  webRequest.SendWebRequest();

        //     responseCode = (HttpStatus)webRequest.responseCode;
        //}

        var request = new UnityWebRequest("https://phpstack-1076337-5399863.cloudwaysapps.com/api/classification");
        request.method = UnityWebRequest.kHttpVerbPOST;
        
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        //Debug.Log(request.uploadHandler.ToString());
        Debug.Log("Status Code: " + request.responseCode);







    
        //UnityWebRequest request = UnityWebRequest.Post("https://phpstack-1076337-5399863.cloudwaysapps.com/api/classification",jsonHS); //new UnityWebRequest("https://phpstack-1076337-5399863.cloudwaysapps.com/game/classification/ZHVxZUtGF4E0wzz0400BRy8imjHDgZPmL5m5UD5VYBUCstloOUH2sSbbS9ef", "POST");

        // yield return request.SendWebRequest();

        // if (request.result == UnityWebRequest.Result.Success)
        // {
        //     Debug.Log("Respuesta: " + request.downloadHandler.text);
        // }
        // else
        // {
        //     Debug.LogError("Error: " + request.error);
        // }

    }

    [ContextMenu("Win Game")]
    public void Win()
    {
        //File.WriteAllText(pathUserData, JsonUtility.ToJson(new Data(),true));
        Data data= new Data();
        string json = File.ReadAllText(pathUserData);
        data = JsonUtility.FromJson<Data>(json);


        data = RegistryUpgrades(data);
        data.levelsCompleted[levelNumber] = true;
        data.levelsHighScore[levelNumber] = totalPoints;

        StartCoroutine(PostAPi());

        json = JsonUtility.ToJson(data,true);
        File.WriteAllText(pathUserData, json);

        UIManager.instance.DisplayWinPanel();
        Pause();

    }

    private Data RegistryUpgrades(Data data)
    {
        //Data data = new Data();
    

        string json = File.ReadAllText(pathUserData);
        data = JsonUtility.FromJson<Data>(json);
        
        
        

        foreach (var upgrade in UpgradesManager.instance.upgrades)
        {
            if (!data.discoveredUpgrades.Contains(upgrade.name))
            {
                data.discoveredUpgrades.Add(upgrade.name);
            }
        }
        return data;
        
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

}
