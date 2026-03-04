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

        StartCoroutine(GetFromApi("https://phpstack-1076337-5399863.cloudwaysapps.com/api/classification/ZHVxZUtGF4E0wzz0400BRy8imjHDgZPmL5m5UD5VYBUCstloOUH2sSbbS9ef", (reply) =>{ 
        
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

    

    public void GoToValorate()
    {
        StartCoroutine(VerifyValoration((reply) =>{
            //Debug.Log(reply);
            if (!reply)
            {   
                ratedText.SetActive(false);
                ratePanel.SetActive(true);
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
            StartCoroutine(PostValoration());
            ratePanel.SetActive(false);

        }
        
    }

    public IEnumerator PostValoration()
    {
        Data data = new Data();
        string json = File.ReadAllText(pathUserData);
        data = JsonUtility.FromJson<Data>(json);
        RateData dataToRate = new RateData();

        dataToRate.api_token = "ZHVxZUtGF4E0wzz0400BRy8imjHDgZPmL5m5UD5VYBUCstloOUH2sSbbS9ef";
        dataToRate.email = data.email;
        dataToRate.name = data.username;
        dataToRate.general = (int)generalSlider.value;
        dataToRate.jugabilitat = (int)jugabilitatSlider.value;
        dataToRate.dificultat = (int)dificultatSlider.value;
        dataToRate.grafics = (int)graficsSlider.value;
        dataToRate.concordnacia = (int)concordanciaSlider.value;

        string jsonHS = JsonUtility.ToJson(dataToRate);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonHS);

        //Debug.Log(jsonHS);

        var request = new UnityWebRequest("https://phpstack-1076337-5399863.cloudwaysapps.com/api/rateGame");
        request.method = UnityWebRequest.kHttpVerbPOST;
            
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        Debug.Log(request.responseCode);
    }
    public IEnumerator VerifyValoration(Action<bool> callback)
    {


        Data data = new Data();
        string json = File.ReadAllText(pathUserData);
        data = JsonUtility.FromJson<Data>(json);


        VerifyData postData= new VerifyData();

        postData.api_token = "ZHVxZUtGF4E0wzz0400BRy8imjHDgZPmL5m5UD5VYBUCstloOUH2sSbbS9ef";
        postData.name = data.username;
        postData.email = data.email;

        string jsonHS = JsonUtility.ToJson(postData);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonHS);

        //Debug.Log(jsonHS);

        var request = new UnityWebRequest("https://phpstack-1076337-5399863.cloudwaysapps.com/api/verify");
        request.method = UnityWebRequest.kHttpVerbPOST;
        
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        //Debug.Log(request.uploadHandler.ToString());
        //Debug.Log("Status Code: " + request.responseCode);
        //Debug.Log("Has Rated:" + request.downloadHandler.text);

        
        var replyjson = JsonUtility.FromJson<VerifiedData>(request.downloadHandler.text);
       
        callback(replyjson.rated);

    
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

    
        

    public void Play()
    {
        SceneManager.LoadScene(levels[actLevelSelected].levelSceneId);
    }


    public void Exit()
    {
        Application.Quit();
    }


}
