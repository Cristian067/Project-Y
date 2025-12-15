using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class UpgradeEffects
{
    public UpgradeSO sideToSideEffect;
    public UpgradeSO magicMirror;
    public UpgradeSO barrier;
    public UpgradeSO chargedShoot;
}


public class UpgradesManager : MonoBehaviour
{

    public static UpgradesManager instance { get; private set; }


    public UpgradeEffects effects;

    [SerializeField] private GameObject upgradesContainer;
    [SerializeField] private GameObject UpgradePrefab;

    private bool isCustom;

    [SerializeField] private UpgradeSO[] allTheUpgrades;


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
        




    }

    // Update is called once per frame
    void Update()
    {

    }


    public UpgradeSO GetRandomUpgrade(int type, int goodOrBad)
    {
        //type = 0 = modificacion de estadisticas
        //type = 1 = efectos
        //type = 2 = especial

        //TODO: Hacer un random para elegir si la mejora es positiva o negativa para que no sea beneficioso solo para un lado
        if (!isCustom)
        {
        //int randomSide = Random.Range(0, 2);
        if (type == 0)
        {
            if (goodOrBad == 0)
            {
                List<UpgradeSO> goodUpgrade = new List<UpgradeSO>();
                foreach (UpgradeSO upgrade in allTheUpgrades)
                {
                    if (upgrade.alignment == UpgradeSO.Alignment.Positive && upgrade.type == UpgradeSO.UpgradeType.StatModification)
                    {
                        goodUpgrade.Add(upgrade);
                    }
                }
                return goodUpgrade[Random.Range(0, goodUpgrade.Count)];
            }
            else
            {
                List<UpgradeSO> badUpgrade = new List<UpgradeSO>();

                foreach (UpgradeSO upgrade in allTheUpgrades)
                {
                    if (upgrade.alignment == UpgradeSO.Alignment.Negative && upgrade.type == UpgradeSO.UpgradeType.StatModification)
                    {
                        badUpgrade.Add(upgrade);
                    }
                }
                return badUpgrade[Random.Range(0, badUpgrade.Count)];
            }
        }

        else if (type == 1)
        {
            if (goodOrBad == 0)
            {
                List<UpgradeSO> goodUpgrade = new List<UpgradeSO>();
                foreach (UpgradeSO upgrade in allTheUpgrades)
                {
                    if (upgrade.alignment == UpgradeSO.Alignment.Positive && upgrade.type == UpgradeSO.UpgradeType.Effect)
                    {
                        goodUpgrade.Add(upgrade);
                    }
                }
                return goodUpgrade[Random.Range(0, goodUpgrade.Count)];
            }
            else
            {
                List<UpgradeSO> badUpgrade = new List<UpgradeSO>();

                foreach (UpgradeSO upgrade in allTheUpgrades)
                {
                    if (upgrade.alignment == UpgradeSO.Alignment.Negative && upgrade.type == UpgradeSO.UpgradeType.Effect)
                    {
                        badUpgrade.Add(upgrade);
                    }
                }
                try
                {
                    return badUpgrade[Random.Range(0, badUpgrade.Count)];

                }

                catch
                {
                    return null;
                    //Debug.Log("error con mejora");
                }
                
                
                
                }
        }
        else
        {
            //if (goodOrBad == 0)
            //{
                List<UpgradeSO> goodUpgrade = new List<UpgradeSO>();
                foreach (UpgradeSO upgrade in allTheUpgrades)
                {
                    if (upgrade.alignment == UpgradeSO.Alignment.Positive && upgrade.type == UpgradeSO.UpgradeType.Special)
                    {
                        goodUpgrade.Add(upgrade);
                    }
                }
                return goodUpgrade[Random.Range(0, goodUpgrade.Count)];
            //}
            // else
            // {
            //     List<UpgradeSO> badUpgrade = new List<UpgradeSO>();

            //     foreach (UpgradeSO upgrade in allTheUpgrades)
            //     {
            //         if (upgrade.alignment == UpgradeSO.Alignment.Negative && upgrade.type == UpgradeSO.UpgradeType.Effect)
            //         {
            //             badUpgrade.Add(upgrade);
            //         }
            //     }
            //     return badUpgrade[Random.Range(0, badUpgrade.Count)];
            // }
        }
        //return allTheUpgrades[Random.Range(0, allTheUpgrades.Length)];

        }
        else
        {
            return null;
        }
    }


    public UpgradeSO GetRandomUpgradesFixed(int type, int goodOrBad, UpgradeSO[] pull)
    {
        //type = 0 = modificacion de estadisticas
        //type = 1 = efectos
        //type = 2 = especial
        if(goodOrBad == 0)
        {
            List<UpgradeSO> goodUpgrade = new List<UpgradeSO>();
            foreach (UpgradeSO upgrade in pull)
            {
                if (upgrade.alignment == UpgradeSO.Alignment.Positive)
                {
                    goodUpgrade.Add(upgrade);
                }
            }
            return goodUpgrade[Random.Range(0, goodUpgrade.Count)];

        }
        else if (goodOrBad == 1)
        {
            List<UpgradeSO> goodUpgrade = new List<UpgradeSO>();
            foreach (UpgradeSO upgrade in pull)
            {
                if (upgrade.alignment == UpgradeSO.Alignment.Negative)
                {
                    goodUpgrade.Add(upgrade);
                }
            }
            return goodUpgrade[Random.Range(0, goodUpgrade.Count)];

        }
        return null;
    }
    [ContextMenu("Display upgrades screen")]
    public void DisplayUpgrades()
    {
        GameManager.instance.Pause();
        UpgradeSO[] allyUpgrades = new UpgradeSO[3];
        UpgradeSO[] enemyUpgrades = new UpgradeSO[3];

        List<UpgradeSO> upgradePull = allTheUpgrades.ToList<UpgradeSO>();


        foreach(UpgradeSO upgrade in GameManager.instance.GetUpgrades())
        {
            if (upgradePull.Contains(upgrade))
            {
                upgradePull.Remove(upgrade);
            }
        }

        //get selection for player

        for (int i = 0; i < 3; i++)
        {   
            allyUpgrades[i] = GetRandomUpgradesFixed(Random.Range(0,3),Random.Range(0, 2),upgradePull.ToArray());
            enemyUpgrades[i] = GetRandomUpgradesFixed(0, Random.Range(0, 2),upgradePull.ToArray());
            //Debug.Log(allyUpgrades[i]);
            //Debug.Log(enemyUpgrades[i]);

            //GameObject card = Instantiate(UpgradePrefab, upgradesContainer.transform);
            //card.GetComponent<CardUpgrade>().Set(allyUpgrades[i], enemyUpgrades[i]);


            //if (i== 1)
            //{
            //    card.GetComponent<Button>().Select();
            //}

            
            //StartCoroutine(CheckUpgradeDisponobility(enemyUpgrades, true));


        }

        StartCoroutine(CheckUpgradeDisponobility(allyUpgrades, enemyUpgrades, upgradePull.ToArray()));


        //isCustom = false;

        //Instantiate(UpgradePrefab, upgradesContainer.transform);
        //Button select = Instantiate(UpgradePrefab, upgradesContainer.transform).GetComponent<Button>();
        //select.Select();
        //Instantiate(UpgradePrefab, upgradesContainer.transform);

        //Instantiate(UpgradePrefab, canvas.transform);
    }


    private void SendUpgrades(UpgradeSO[] ally, UpgradeSO[] enemy)
    {
        for (int i = 0; i < 3; i++)
        {

            //Debug.Log(allyUpgrades[i]);
            //Debug.Log(enemyUpgrades[i]);

            GameObject card = Instantiate(UpgradePrefab, upgradesContainer.transform);
            card.GetComponent<CardUpgrade>().Set(ally[i], enemy[i]);


            if (i == 1)
            {
                card.GetComponent<Button>().Select();
            }
            
        }
    }


    private IEnumerator CheckUpgradeDisponobility(UpgradeSO[] ally,UpgradeSO[] enemy,UpgradeSO[] pull)
    {
        for (int i = 0; i < 3; i++)
        {
            try
            {

            
                while (ally[i] == ally[i - 1] || ally[i] == ally[i - 2])
                {
                    ally[i] = GetRandomUpgradesFixed(Random.Range(0, 3), Random.Range(0, 1),pull);
                }
            }

            catch { }

            while (ally[i].type == UpgradeSO.UpgradeType.Special && ally[i] == GameManager.instance.GetSpecial())
            {
                ally[i] = GetRandomUpgradesFixed(Random.Range(0, 3), Random.Range(0, 1), pull);
            }



        }

        SendUpgrades(ally, enemy);
        yield return null;

    }

        

    

    public void GiveUpgrade()
    {
        
    }
    // public void DisplayUpgradesCustom(string upgrade1, string upgrade2, string upgrade3)
    // {
    //     isCustom = true;
    //     GameManager.instance.Pause();
    //     GameObject upgrade1Card = Instantiate(UpgradePrefab, upgradesContainer.transform);
    //     foreach (UpgradeSO upgrade in allTheUpgrades)
    //     {
    //         Debug.Log(upgrade1 +" " + upgrade.name);
    //         if (upgrade1 == upgrade.name)
    //         {
    //             upgrade1Card.GetComponent<UpgradeSO>().special = upgrade.special;
    //         }
    //     }
    //     if(upgrade1Card.GetComponent<UpgradeSO>().special == null)
    //     {
    //         Destroy(upgrade1Card);
    //     }

    //     GameObject upgrade2Card = Instantiate(UpgradePrefab, upgradesContainer.transform);
    //     upgrade2Card.GetComponent<UpgradeSO>().type = UpgradeSO.UpgradeType.Special;
    //     foreach (UpgradeSO upgrade in allTheUpgrades)
    //     {
    //         if (upgrade2 == upgrade.name)
    //         {
    //             upgrade2Card.GetComponent<UpgradeSO>().special = upgrade.special;
    //         }
    //     }
    //     if(upgrade2Card.GetComponent<UpgradeSO>().special == null)
    //     {
    //         Destroy(upgrade2Card);
    //     }
        
    //     upgrade2Card.GetComponent<Button>().Select();

    //     GameObject upgrade3Card = Instantiate(UpgradePrefab, upgradesContainer.transform);

    //     foreach (UpgradeSO upgrade in allTheUpgrades)
    //     {
    //         if (upgrade3 == upgrade.name)
    //         {
    //             upgrade3Card.GetComponent<UpgradeSO>().special = upgrade.special;
    //         }
    //     }
    //     if(upgrade3Card.GetComponent<UpgradeSO>().special == null)
    //     {
    //         Destroy(upgrade3Card);
    //     }

    //     //Instantiate(UpgradePrefab, canvas.transform);
    // }

    public void UnDisplayeUpgrades()
    {
        Debug.Log(upgradesContainer.transform.childCount);
        //Debug.Log(gameObject.transform.GetChild(1).gameObject);

        foreach (Transform child in upgradesContainer.transform)
        {
            Destroy(child.gameObject);
        }
        GameManager.instance.Unpause(true);
        UIManager.instance.RefreshStatsUi();
        //     for(int i = 0; i < upgradesContainer.transform.childCount-1; i++)
        //     {
        //         Destroy(transform.GetChild(i).gameObject);
        //     }
    }





}


public class Effects 
{


    public void Shield()
    {

    }

}
