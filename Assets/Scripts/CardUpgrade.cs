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

        int randomType = Random.Range(0, 3);
        //type = 0 = modificacion de estadisticas
        //type = 1 = efectos
        //type = 2 = especial

        int randomSide = Random.Range(0, 2);


        allyUpgrade = UpgradesManager.instance.GetRandomUpgrade(randomType,randomSide);
        enemyUpgrade = UpgradesManager.instance.GetRandomUpgrade(randomType,randomSide);

        if (allyUpgrade.type == UpgradeSO.UpgradeType.Effect)
        {
            //enemyUpgrade = null;
            allyText.text = allyUpgrade.description;
            enemyText.text = "The boss " + enemyUpgrade.description;

        }
        else if (allyUpgrade.type == UpgradeSO.UpgradeType.Special)
        {
            allyText.text = allyUpgrade.description;
            enemyUpgrade = null;
        }
        else
        {
            allyText.text = "You " + allyUpgrade.description;
            enemyText.text = "The boss " + enemyUpgrade.description;
        }
        


    }
    
    public void Choosed()
    {
        GameManager.instance.AdquireUpgrade(0,allyUpgrade);
        GameManager.instance.AdquireUpgrade(1, enemyUpgrade);
        UpgradesManager.instance.UnDisplayeUpgrades();
    }


}
