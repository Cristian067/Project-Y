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
    [SerializeField] private GameObject pausePanel;

    [SerializeField] private Button buttonWinPanel;
    [SerializeField] private Button buttonLosePanel;

    [SerializeField] private Button buttonPausePanel;


    [Header("Dialogue Things")]

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueName;
    [SerializeField] private TextMeshProUGUI dialogueText;


    [Header("Boss Things")]

    [SerializeField] private GameObject bossThings;
    [SerializeField] private TextMeshProUGUI bossName;
    [SerializeField] private Slider bossMaxHealth;


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

    public void DisplayBossThings(string name,float health)
    {
        bossThings.SetActive(true);
        bossName.text = name;
        bossMaxHealth.maxValue = health;
        bossMaxHealth.value = health;
    }

    public void HurtHealthBar(float health)
    {
        bossMaxHealth.value = health;
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


    public void DisplayDialogue(string dialogue, string who)
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = dialogue;
        dialogueName.text = who;

        
    }

    public void UndisplayDialogue()
    {
        dialoguePanel.SetActive(false);
    }
    

    public void DisplayPauseMenu()
    {
        pausePanel.SetActive(true);
        buttonPausePanel.Select();
    }

    public void UndisplayPauseMenu()
    {
        pausePanel.SetActive(false);
        
    }

}
