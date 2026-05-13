using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class Levels
{
    public string levelName;
    public string levelDescription;
    public Sprite levelImg;
    public int levelSceneId;

    public bool isLocked;

}


public class MainManager : MonoBehaviour
{
    public Levels[] levels;
    [SerializeField] private int actLevelSelected;

    [SerializeField] private Image levelImage;
    [SerializeField] private TextMeshProUGUI levelNameText;
    [SerializeField] private TextMeshProUGUI levelNameLeaderboardText;
    [SerializeField] private TextMeshProUGUI levelDescriptionText;
    [SerializeField] private TextMeshProUGUI levelHighScoreText;
    [SerializeField] private TextMeshProUGUI levelNumberText;
    [SerializeField] private TextMeshProUGUI levelNumberLeaderboardText;

    [SerializeField] private GameObject[] menusPanel;
    [SerializeField] private GameObject lockedLevelPanel;
   

    [SerializeField] private Button selectedMainButton;

    public DataFromApi leaderboard;
    public GameObject leaderboardContentGo;

    [Header("LogIn")]
    [SerializeField] private GameObject enterUsernamePanel;
    [SerializeField] private TMP_InputField enterUsernameField;
    [SerializeField] private TMP_InputField enterEmailField;

    [Header("Rate")]
    public GameObject ratePanel;
    public GameObject ratedText;
    public Slider generalSlider;
    public Slider jugabilitatSlider;
    public Slider dificultatSlider;
    public Slider graficsSlider;
    public Slider concordanciaSlider;

    [SerializeField] private Button playButton;


    [SerializeField] private TextMeshProUGUI usernameDisplay;

    [SerializeField] private UpgradesDBSo upgradesDatabase;

    [Header("Encyclopedia")]

     [SerializeField]private TextMeshProUGUI nameText;
     [SerializeField]private Image encyclopediaImage;
     [SerializeField]private TextMeshProUGUI descriptionText;

    [SerializeField] private GameObject upgradesSlotsPanel;
    [SerializeField] private GameObject charactersSlotsPanel;
    [SerializeField] private GameObject storySlotsPanel;

    private string pathUserData = "save/UserData.json";
    private string apiUrl;
    private string token;


    void Start()
    {
        apiUrl = Resources.Load<NetworkDataSO>("ScriptableObjects/NetworkData").apiUrl;
        token = Resources.Load<NetworkDataSO>("ScriptableObjects/NetworkData").token;

        GoToMenu(0);

        Data data = new Data();

        if (!File.Exists(pathUserData))
        {
            Debug.Log("No existe el archivo, se va a crear uno nuevo");
            Directory.CreateDirectory("save");
            data.levelsCompleted[0] = false;
            data.charactersMet.Add("Yang");
            //data.charactersMet = new List<string>();
            //data.charactersMet.Add("Yang");
            string json = JsonUtility.ToJson(data,true);
            File.WriteAllText(pathUserData,json); 
            
        }
        else
        {
            string json = File.ReadAllText(pathUserData);
            data = JsonUtility.FromJson<Data>(json);
            
        }
        
        if(data.username == null || data.username == "" || data.email == null || data.email == "")
        {
            GameObject.Find("EventSystem").GetComponent<DisableMouse>().enabled = false;
            //GetComponent<GraphicRaycaster>().enabled = true;
            enterUsernamePanel.SetActive(true);
            enterUsernameField.Select();
        }
        else
        {
            usernameDisplay.text = data.username;
        }
        
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
    }

    public void LogOut()
    {
        Data data = new Data();
        GameObject.Find("EventSystem").GetComponent<DisableMouse>().enabled = false;
        enterUsernamePanel.SetActive(true);
        enterUsernameField.Select();
    }

    public void DontLogIn()
    {
        enterUsernamePanel.SetActive(false);

        string json = File.ReadAllText(pathUserData);
        Data data = JsonUtility.FromJson<Data>(json);
        
        if((data.username == null || data.username == "") && (data.email == null || data.email == ""))
        {
            data.username = "User";
            data.email = "anonimous@gmail.com";
        }
        

        string json2 = JsonUtility.ToJson(data,true);
        File.WriteAllText(pathUserData,json2); 

        usernameDisplay.text = data.username;

    }

    public void SaveInfo()
    {
        string json = File.ReadAllText(pathUserData);
        Data data = JsonUtility.FromJson<Data>(json);
        
        data.username = enterUsernameField.text;
        data.email = enterEmailField.text;

        string json2 = JsonUtility.ToJson(data,true);
        File.WriteAllText(pathUserData,json2); 

        GameObject.Find("EventSystem").GetComponent<DisableMouse>().enabled = true;
        enterUsernamePanel.SetActive(false);

        usernameDisplay.text = data.username;
        //GetComponent<GraphicRaycaster>().enabled = false;

    }

    public void GoToMenu(int panel)
    {
        ratedText.SetActive(false);
        foreach (GameObject go in menusPanel)
        {
            go.SetActive(false);
        }

        menusPanel[panel].SetActive(true);

    }
    

    
    
    public void RefreshLeaderBoard(int level = 0)
    {

        ChangeLevel(level);

        levelNameLeaderboardText.text = levels[actLevelSelected].levelName;
        levelNumberLeaderboardText.text = $"Level {actLevelSelected}";


        DataFromApi newLeaderboard = new DataFromApi();
        newLeaderboard.data = new List<Scores>();

        foreach (Transform child in leaderboardContentGo.transform)
        {
            Destroy(child.gameObject);
        }

        StartCoroutine(ApiCalls.GetLeaderBoard($"https://phpstack-1076337-5399863.cloudwaysapps.com/api/classification/{token}", (reply) =>{ 
        
        leaderboard = JsonUtility.FromJson<DataFromApi>(reply);

        foreach (Scores api in leaderboard.data)
        {
            string[] splitArray =  api.name.Split(char.Parse("_"));
            //int userLevel = 

            Debug.Log(int.Parse(splitArray[0]));

            if(int.Parse(splitArray[0]) == actLevelSelected)
            {
                newLeaderboard.data.Add(api);
            }
        }

        leaderboard = newLeaderboard;

        for(int i = 0; i < leaderboard.data.Count; i++)
        {
            string[] splitArray =  leaderboard.data[i].name.Split(char.Parse("_"));

            var slot = Instantiate(Resources.Load<GameObject>("Prefabs/InfoLeaderboard"),leaderboardContentGo.transform);

            slot.GetComponent<InfoLeaderboard>().SendInfo(i+1,splitArray[1],leaderboard.data[i].puntuacion);

        }
        
        }));
        
    }

    
    public void SelectButton(Button button)
    {
        button.Select();
        
    }

    

    public void GoToValorate()
    {
        StartCoroutine(ApiCalls.VerifyValoration(pathUserData,(reply) =>{
            //Debug.Log(reply);
            if (!reply)
            {   
                ratedText.SetActive(false);
                ratePanel.SetActive(true);
                generalSlider.Select();
                Debug.Log("No ha valorado");

            }
            else
            {
                ratedText.SetActive(true);
            }
        
        }));
    }

    public void Later()
    {
        ratePanel.SetActive(false);
    }
    public void Rate()
    {
        if(generalSlider.value == 0|| jugabilitatSlider.value == 0 ||dificultatSlider.value == 0 || graficsSlider.value == 0|| concordanciaSlider.value == 0)
        {
            Debug.Log("Tienes que valorar todos los valores");
            
        }
        else
        {
            StartCoroutine(ApiCalls.PostValoration(pathUserData,generalSlider.value,jugabilitatSlider.value,dificultatSlider.value,graficsSlider.value,concordanciaSlider.value));
            ratePanel.SetActive(false);

        }
        
    }

    
    public void ExportLog()
    {
        Log.ExportLog();
    }

    private void ChangeLevel(int upOrDown)
    {
        actLevelSelected += upOrDown;
        if (actLevelSelected > levels.Length - 1)
        {
            actLevelSelected = 0;
        }
        else if (actLevelSelected < 0)
        {
            actLevelSelected = levels.Length - 1;
        }
    }

    public void RefreshLevel(int upOrDown)
    {

        Data data = new Data();
        
        string json = File.ReadAllText(pathUserData);
        data = JsonUtility.FromJson<Data>(json);

        ChangeLevel(upOrDown);

        levelImage.sprite = levels[actLevelSelected].levelImg;
        levelNameText.text = levels[actLevelSelected].levelName;
        levelDescriptionText.text = levels[actLevelSelected].levelDescription;
        levelHighScoreText.text = $"HighScore: {data.levelsHighScore[actLevelSelected]}";
        levelNumberText.text = $"Level {actLevelSelected}";

        if (levels[actLevelSelected].isLocked)
        {
            if (!data.levelsCompleted[actLevelSelected-1])
            {
                lockedLevelPanel.SetActive(true);
                playButton.interactable = false;
            }
        }

        else
        {
            lockedLevelPanel.SetActive(false); 
            playButton.interactable = true;
        } 
    }

    public void CleanEncyclopedia()
    {
        foreach (Transform child in upgradesSlotsPanel.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in charactersSlotsPanel.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in storySlotsPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }
    
    public void RefreshEncyclopedia(int type = 0)
    {
        CleanEncyclopedia();

        if(type == 0)
        {
            upgradesSlotsPanel.SetActive(true);
            charactersSlotsPanel.SetActive(false);
            storySlotsPanel.SetActive(false);
            foreach (var upgrade in upgradesDatabase.upgradeSOs)
            {
                if (upgrade.whoToAdd == UpgradeSO.Who.Player || upgrade.whoToAdd == UpgradeSO.Who.Both)
                {
                    GameObject slot = Instantiate(Resources.Load<GameObject>("UpgradeEncyclopedia"), upgradesSlotsPanel.transform);
                    slot.GetComponent<EncyclopediaButtonHandler>().upgrade = upgrade;
                    slot.GetComponent<EncyclopediaButtonHandler>().description_text = descriptionText;
                    slot.GetComponent<EncyclopediaButtonHandler>().upgradeImage = encyclopediaImage;
                    slot.GetComponent<EncyclopediaButtonHandler>().name_text = nameText;
                }
                //else return;

            }
            upgradesSlotsPanel.transform.GetChild(0).GetComponent<Button>().Select();
        }
        else  if(type == 1)
        {
            upgradesSlotsPanel.SetActive(false);
            charactersSlotsPanel.SetActive(true);
            storySlotsPanel.SetActive(false);
            foreach (var character in upgradesDatabase.characterSOs)
            {
               
                    GameObject slot = Instantiate(Resources.Load<GameObject>("UpgradeEncyclopedia"), charactersSlotsPanel.transform);
                    slot.GetComponent<EncyclopediaButtonHandler>().character = character;
                    slot.GetComponent<EncyclopediaButtonHandler>().slotType = EncyclopediaButtonHandler.Type.Character;
                    slot.GetComponent<EncyclopediaButtonHandler>().name_text = nameText;
                    slot.GetComponent<EncyclopediaButtonHandler>().description_text = descriptionText;
                    slot.GetComponent<EncyclopediaButtonHandler>().upgradeImage = encyclopediaImage;
                    
                
                //else return;

            }
            charactersSlotsPanel.transform.GetChild(0).GetComponent<Button>().Select();
        }
        
        
        
    }
        

    public void Play()
    {
        SceneManager.LoadScene(levels[actLevelSelected].levelSceneId);
    }


    public void Exit()
    {
        Application.Quit();
    }

    void OnApplicationQuit()
    {
        //Log.AddToLog("User");
        Log.ExportLog();
    }


}
