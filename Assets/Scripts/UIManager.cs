using System.Collections;
using System.Collections.Generic;
using TMPro;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set;}
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private Slider livesSlider;
    [SerializeField] private Slider specialSlider;
    [SerializeField] private TextMeshProUGUI specialText;
    [SerializeField] private TextMeshProUGUI totalPointsText;


    [SerializeField] private TextMeshProUGUI upgradesListText;
    [SerializeField] private TextMeshProUGUI currentSpecialText;
    [SerializeField] private Image currentSpecialImage;

    [SerializeField] private Slider upgradeProgressSlider;
    [SerializeField] private Slider specialCooldownSlider;


    [Header("Ui Panels")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionsPanel;

    [SerializeField] private Button buttonWinPanel;
    [SerializeField] private Button buttonLosePanel;
    [SerializeField] private Button buttonPausePanel;
    [SerializeField] private Button buttonOptionsPanel;


    [Header("Dialogue Things")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueName;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private float textTime = 0.02f;


    [Header("Boss Things")]
    [SerializeField] private GameObject bossThings;
    [SerializeField] private TextMeshProUGUI bossName;
    [SerializeField] private Slider bossMaxHealth;


    [Header("Upgrades things")]
    [SerializeField] private GameObject upgradesContainer;
    [SerializeField] private GameObject UpgradePrefab;

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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        currentSpecialText.text = "";
        currentSpecialImage.sprite = null;

        currentSpecialImage.color = new Color(0, 0, 0, 0);
        //RefreshStatsUi();
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        winPanel.SetActive(true);
        buttonWinPanel.Select();
    }

    public void DisplayLosePanel()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        losePanel.SetActive(true);
        buttonLosePanel.Select();
    }

    public void RefreshStatsUi()
    {
        livesSlider.value = GameManager.instance.GetPlayerLives();
        //livesText.text = $"{GameManager.instance.GetPlayerLives()}/"+GameManager.instance.maxLives;
        specialSlider.value = GameManager.instance.specials;
        specialText.text = $"{GameManager.instance.specials}/"+ GameManager.instance.maxSpecials;
        totalPointsText.text = $"{GameManager.instance.GetTotalPoints()}";

        upgradeProgressSlider.value = GameManager.instance.GetPoints();
        upgradeProgressSlider.maxValue = GameManager.instance.GetPointsToUpgrade();

        upgradesListText.text = "";
        foreach(var upgrade in UpgradesManager.instance.playerUpgrades)
        {
            upgradesListText.text += "- "+upgrade.name + "\n";
        }

        if (UpgradesManager.instance.special != null)
        {
            currentSpecialText.text = $"{UpgradesManager.instance.special.name}";
            currentSpecialImage.sprite = UpgradesManager.instance.special.UpgradeImage;
            currentSpecialImage.color = new Color(1, 1, 1, 1);
        }
        else
        {
            currentSpecialText.text = $"";
            currentSpecialImage.sprite = null;
            currentSpecialImage.color = new Color(1, 1, 1, 0);
        }
        
    }

    private Coroutine TextAnimationCoroutine;

    public void DisplayDialogue(string dialogue, string who)
    {
        dialoguePanel.SetActive(true);
        
        //dialogueText.text = dialogue;
        dialogueName.text = who;
        if (TextAnimationCoroutine != null)
        {
            StopCoroutine(TextAnimationCoroutine);
        }
        TextAnimationCoroutine = StartCoroutine(DialogueAnimation(dialogue));

    }

    private IEnumerator DialogueAnimation(string dialogue)
    {
        dialogueText.text = "";
        foreach(char c in dialogue.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textTime);
        }
    }

    public void UndisplayDialogue()
    {
        dialoguePanel.SetActive(false);
    }
    
    public void DisplayPauseMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        pausePanel.SetActive(true);
        buttonPausePanel.Select();
    }

    public void UndisplayPauseMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        pausePanel.SetActive(false);
        
    }

    public void UnDisplayeUpgrades()
    {
        //Debug.Log(upgradesContainer.transform.childCount);
        foreach (Transform child in upgradesContainer.transform)
        {
            Destroy(child.gameObject);
        }
        GameManager.instance.Unpause(true);
        RefreshStatsUi();

    }

    public void UpdateCooldownVisual(float time, float specialCooldown)
    {
        specialCooldownSlider.value = time;
        specialCooldownSlider.maxValue = specialCooldown;
        
    }

    public void GoToOptions()
    {
        optionsPanel.SetActive(true);
        buttonOptionsPanel.Select();
    }
    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        buttonPausePanel.Select();
    }

    public void SendUpgrades(UpgradeSO[] ally, UpgradeSO[] enemy)
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject card = Instantiate(UpgradePrefab, upgradesContainer.transform);
            card.GetComponent<CardUpgrade>().Set(ally[i], enemy[i]);

            if (i == 1)
            {
                StartCoroutine(SelectUpgrade(card));
                //card.GetComponent<Button>().Select();
            }
            
        }
    }

    public IEnumerator SelectUpgrade(GameObject upgrade)
    {
        yield return new WaitForSeconds(0.75f);
        upgrade.GetComponent<Button>().Select();
        //yield return null;
    }

}
