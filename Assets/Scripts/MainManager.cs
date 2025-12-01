using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class Levels
{
    public string levelName;
    public string levelDescription;
    public Sprite levelImg;
    public int levelSceneId;

}


public class MainManager : MonoBehaviour
{


    public Levels[] levels;
    [SerializeField] private int actLevelSelected;


    [SerializeField] private Image levelImage;
    [SerializeField] private TextMeshProUGUI levelNameText;
    [SerializeField] private TextMeshProUGUI levelDescriptionText;
    [SerializeField] private TextMeshProUGUI levelNumberText;



    [SerializeField] private GameObject[] menusPanel;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GoToMenu(0);

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


    public void RefreshLevel(int upOrDown)
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

        levelImage.sprite = levels[actLevelSelected].levelImg;
        levelNameText.text = levels[actLevelSelected].levelName;
        levelDescriptionText.text = levels[actLevelSelected].levelDescription;
        levelNumberText.text = $"Level {actLevelSelected}";


    }
    

    public void Play()
    {
        SceneManager.LoadScene(levels[actLevelSelected].levelSceneId);
    }


}
