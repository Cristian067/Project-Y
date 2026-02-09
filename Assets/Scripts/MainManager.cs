using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
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
    [SerializeField] private TextMeshProUGUI levelDescriptionText;
    [SerializeField] private TextMeshProUGUI levelHighScoreText;
    [SerializeField] private TextMeshProUGUI levelNumberText;



    [SerializeField] private GameObject[] menusPanel;

    [SerializeField] private GameObject lockedLevelPanel;

    [SerializeField] private Button selectedMainButton;


    public DataFromApi leaderboard;

    private string pathUserData = "save/UserData.json";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        selectedMainButton.Select();
        GoToMenu(0);
        RefreshLeaderBoard(1);

        //Screen.SetResolution(800, 600,false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        public DataApi[] data;
    }
    [Serializable]
    public class DataApi
    {
        public string name;
        public int puntuacion;
    }


    
    public void RefreshLeaderBoard(int level = 0)
    {

        StartCoroutine(GetFromApi("https://phpstack-1076337-5399863.cloudwaysapps.com/api/classification/ZHVxZUtGF4E0wzz0400BRy8imjHDgZPmL5m5UD5VYBUCstloOUH2sSbbS9ef"));

        
        // foreach (DataApi api in leaderboard.data)
        // {
        //     //Debug.Log(api.name);
        //     string[] splitArray =  api.name.Split(api.name,char.Parse("_"));
        //     Debug.Log(splitArray[0]);
        // }
        //DataFromApi apiData = JsonUtility.FromJson<DataFromApi>(leaderboardRaw);

        
        //leaderboard.Add(apiData.data[1].name);
        
        
    }

    private IEnumerator GetFromApi(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Accept","application/json");
        // request.SetRequestHeader("Accept-Encoding","gzip,deflate,br");
        // //request.SetRequestHeader("Connection","keep-alive");

        request.SetRequestHeader("Content-Type","application/json");

        yield return request.SendWebRequest();

        Debug.Log(request.result);
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Respuesta: " + request.downloadHandler.text);
            //callback(request.downloadHandler.text);
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


    public void RefreshLevel(int upOrDown)
    {

        Data data = new Data();

        if (!File.Exists(pathUserData))
        {
            Directory.CreateDirectory("save");
            data.levelsCompleted[0] = true;
            string json = JsonUtility.ToJson(data,true);
            File.WriteAllText(pathUserData,json); 
            
        }
        else
        {
            string json = File.ReadAllText(pathUserData);
            data = JsonUtility.FromJson<Data>(json);
        }
        


        actLevelSelected += upOrDown;
        if (actLevelSelected > levels.Length - 1)
        {
            actLevelSelected = 0;
        }
        else if (actLevelSelected < 0)
        {
            actLevelSelected = levels.Length - 1;
        }

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
