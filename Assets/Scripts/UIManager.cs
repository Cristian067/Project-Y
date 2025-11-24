using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance { get; private set;}
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI totalPointsText;
    [SerializeField] private TextMeshProUGUI upgradesListText;
    [SerializeField] private TextMeshProUGUI currentSpecialText;

    [SerializeField] private Slider upgradeProgressSlider;


    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    [SerializeField] private Button buttonWinPanel;
    [SerializeField] private Button buttonLosePanel;


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
        winPanel.SetActive(false);
        losePanel.SetActive(false); 

        currentSpecialText.text = "";


        //RefreshStatsUi();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayWinPanel()
    {
        winPanel.SetActive(true);
        buttonWinPanel.Select();
    }

    public void DisplayLosePanel()
    {
        losePanel.SetActive(true);
        buttonLosePanel.Select();
    }

    public void RefreshStatsUi()
    {
        livesText.text = $"{GameManager.instance.GetPlayerLives()}/6";
        totalPointsText.text = $"{GameManager.instance.GetTotalPoints()}";

        upgradeProgressSlider.value = GameManager.instance.GetPoints();
        upgradeProgressSlider.maxValue = GameManager.instance.GetPointsToUpgrade();

        upgradesListText.text = "";
        foreach(var upgrade in GameManager.instance.GetUpgrades())
        {
            upgradesListText.text += "- "+upgrade.name + "\n";
        }

        if (GameManager.instance.GetSpecial() != null)
        {
            currentSpecialText.text = $"{GameManager.instance.GetSpecial().name}";
        }
        
    }
    

}
