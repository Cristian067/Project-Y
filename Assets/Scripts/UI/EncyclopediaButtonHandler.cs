using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EncyclopediaButtonHandler : MonoBehaviour, ISelectHandler
{

    [Tooltip("")]
    public string upgradeName;

    [Tooltip("ScriptableObject of the upgrade")]
    public UpgradeSO upgrade;

    public bool unlocked;

    public TextMeshProUGUI name_text;
    public TextMeshProUGUI description_text;


    private string pathUserData = "save/UserData.json";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (upgrade.type == UpgradeSO.UpgradeType.Special)
        {
            ColorBlock colorVar = GetComponent<Button>().colors;
		    colorVar.normalColor = new Color(0.8f,0.8f ,0.1f);
		    colorVar.selectedColor = new Color(0.5f,0.5f ,0.3f);
            GetComponent<Button>().colors = colorVar; //Color.yellow;// = new Color(0.1f,0.5f ,0.5f);
        }
        if (upgrade.type == UpgradeSO.UpgradeType.Effect)
        {
            ColorBlock colorVar = GetComponent<Button>().colors;
		    colorVar.normalColor = new Color(0.2f,0.8f ,0.1f);
		    colorVar.selectedColor = new Color(0.2f,0.5f ,0.4f);
            GetComponent<Button>().colors = colorVar; //Color.yellow;// = new Color(0.1f,0.5f ,0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        CheckIfDiscovered();
    }

    private void CheckIfDiscovered()
    {
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


        if (data.discoveredUpgrades.Contains(upgrade.name))
        {
            unlocked = true;
        }
        else unlocked = false;
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (unlocked)
        {
            name_text.text = upgrade.name;
            description_text.text = upgrade.description;
        }
        else
        {
            name_text.text = "???";
            description_text.text = "???";
        }
        
    }

}
