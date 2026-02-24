using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
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

    [SerializeField] private GameObject enterUsernamePanel;
    [SerializeField] private TMP_InputField enterUsernameField;
    [SerializeField] private TMP_InputField enterEmailField;


    [SerializeField] private TextMeshProUGUI usernameDisplay;

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
            Directory.CreateDirectory("save");
            data.levelsCompleted[0] = false;
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
        
        //data.levelsCompleted[0] = false;
        //string json = JsonUtility.ToJson(data,true);
        //File.WriteAllText(pathUserData,json); 

        GameObject.Find("EventSystem").GetComponent<DisableMouse>().enabled = false;
        //GetComponent<GraphicRaycaster>().enabled = true;
        enterUsernamePanel.SetActive(true);
        enterUsernameField.Select();
    }

    public void SaveInfo()
    {
        Data data = new Data();

        
        string json = File.ReadAllText(pathUserData);
        data = JsonUtility.FromJson<Data>(json);
        
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

        foreach (GameObject go in menusPanel)
        {
            go.SetActive(false);
        }

        menusPanel[panel].SetActive(true);

    }
    [Serializable]
    public class DataFromApi
    {
        public List<DataApi> data;
    }
    [Serializable]
    public class DataApi
    {
        public string name;
        public int puntuacion;
    }

    
    
    public void RefreshLeaderBoard(int level = 0)
    {

        ChangeLevel(level);

        levelNameLeaderboardText.text = levels[actLevelSelected].levelName;
        levelNumberLeaderboardText.text = $"Level {actLevelSelected}";


        DataFromApi newLeaderboard = new DataFromApi();
        newLeaderboard.data = new List<DataApi>();

        foreach (Transform child in leaderboardContentGo.transform)
        {
            Destroy(child.gameObject);
        }

        StartCoroutine(GetFromApi("https://phpstack-1076337-5399863.cloudwaysapps.com/api/classification/ZHVxZUtGF4E0wzz0400BRy8imjHDgZPmL5m5UD5VYBUCstloOUH2sSbbS9ef", (reply) =>{ 
        
        leaderboard = JsonUtility.FromJson<DataFromApi>(reply);

        foreach (DataApi api in leaderboard.data)
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

    private IEnumerator GetFromApi(string url, Action<string> callback)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Accept","application/json");
        request.SetRequestHeader("Content-Type","application/json");

        yield return request.SendWebRequest();

        Debug.Log(request.result);
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Respuesta: " + request.downloadHandler.text);
            callback(request.downloadHandler.text);
            yield return request.downloadHandler.text;
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }
    public void SelectButton(Button button)
    {
        button.Select();
        
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
            }
        }
        
        else lockedLevelPanel.SetActive(false);
    }

    
        

    public void Play()
    {
        SceneManager.LoadScene(levels[actLevelSelected].levelSceneId);
    }


    public void Exit()
    {
        Application.Quit();
    }


}
