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
    public CharacterSO character;
    //public StorySO story;

    public bool unlocked = false;

    public TextMeshProUGUI name_text;
    public Image upgradeImage;
    public TextMeshProUGUI description_text;


    public enum Type
    {
        Upgrade,
        Character,
        Story
    }

    public Type slotType;


    private string pathUserData = "save/UserData.json";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CheckIfDiscovered();

        switch (slotType)
        {
            case Type.Upgrade:
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
                break;
                case Type.Character:
                break;
                case Type.Story:
                break;
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


        switch (slotType)
        {
            case Type.Upgrade:
                if (data.discoveredUpgrades.Contains(upgrade.name))
                {
                    unlocked = true;
                }
                else unlocked = false;
                
                break;
                case Type.Character:
                if (data.charactersMet.Contains(character.name))
                {
                    unlocked = true;
                }
                else 
                {
                unlocked = false;
                Destroy(this.gameObject);
                }

                break;
                case Type.Story:

                break;
        }
        
    }

    public void OnSelect(BaseEventData eventData)
    {
        CheckIfDiscovered();

        switch (slotType)
        {
            case Type.Upgrade:
            description_text.GetComponent<RectTransform>().offsetMax = new Vector2(0,0);
                if (unlocked)
                {
                    name_text.text = upgrade.name;
                    upgradeImage.gameObject.SetActive(true);
                    upgradeImage.sprite = upgrade.UpgradeImage;
                    
                    description_text.text = upgrade.description+"\n"+upgrade.extraDescription;
                }
                else
                {
                    name_text.text = "???";
                    upgradeImage.gameObject.SetActive(false);
                    //upgradeImage.sprite = null;
                    description_text.text = "???";
                }
                
                break;
                case Type.Character:
                description_text.GetComponent<RectTransform>().offsetMax = new Vector2(0,260);
                if (unlocked)
                {
                    name_text.text = character.name;
                    // new Vector2(name_text.preferredWidth+10f,name_text.GetComponent<RectTransform>().sizeDelta.y+260);
                    upgradeImage.gameObject.SetActive(false);
                    upgradeImage.sprite = character.encyclopediaSprite;

                    string baseDescrpt = $"Age: {character.age}\nGender: {character.gender}\nHeight: {character.height}\n\n{character.description}";
                    description_text.text = baseDescrpt;
                }
                else
                {
                    Destroy(this.gameObject);
                }

                break;
                case Type.Story:
                
                break;
        }
        
        
    }

}
