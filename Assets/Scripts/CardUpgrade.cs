using System.Collections;
using TMPro;
using UnityEngine;

public class CardUpgrade : MonoBehaviour
{

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
    

    private void Initialize()
    {
        allyUpgrade = UpgradesManager.instance.GetRandomUpgrade();
        enemyUpgrade = UpgradesManager.instance.GetRandomUpgrade();

        allyText.text = "You "+allyUpgrade.description;
        enemyText.text = "The boss "+enemyUpgrade.description;


    }


}
