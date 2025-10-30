using System.Collections;
using TMPro;
using UnityEngine;

public class CardUpgrade : MonoBehaviour
{

    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }

    public Rarity upgradeRarity;

    [SerializeField] private UpgradeSO allyUpgrade;
    [SerializeField] private UpgradeSO enemyUpgrade;


    [SerializeField] private TextMeshProUGUI allyText;
    [SerializeField] private TextMeshProUGUI enemyText;


    // Start is called before the first frame update
    void Start()
    {
        
        Initialize();
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    

    private void ChooseType()
    {
        int r = Random.Range(0,5);
    }

    private void Initialize()
    {

        int randomSide = Random.Range(0, 2);
        allyUpgrade = UpgradesManager.instance.GetRandomUpgrade(randomSide);
        enemyUpgrade = UpgradesManager.instance.GetRandomUpgrade(randomSide);

        allyText.text = "You "+allyUpgrade.description;
        enemyText.text = "The boss "+enemyUpgrade.description;


    }


}
