using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class UpgradeEffects
{
    public UpgradeSO sideToSideEffect;
    public UpgradeSO magicMirror;
}


public class UpgradesManager : MonoBehaviour
{

    public static UpgradesManager instance { get; private set; }


    public UpgradeEffects effects;

    [SerializeField] private GameObject upgradesContainer;
    [SerializeField] private GameObject UpgradePrefab;

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
        GameManager.instance.Pause();
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
                return badUpgrade[Random.Range(0, badUpgrade.Count)];
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

    [ContextMenu("Display upgrades screen")]
    public void DisplayUpgrades()
    {
        Instantiate(UpgradePrefab, upgradesContainer.transform);
        Button select = Instantiate(UpgradePrefab, upgradesContainer.transform).GetComponent<Button>();
        select.Select();
        Instantiate(UpgradePrefab, upgradesContainer.transform);

        //Instantiate(UpgradePrefab, canvas.transform);
    }

    public void UnDisplayeUpgrades()
    {
        Debug.Log(upgradesContainer.transform.childCount);
        //Debug.Log(gameObject.transform.GetChild(1).gameObject);

        foreach (Transform child in upgradesContainer.transform)
        {
            Destroy(child.gameObject);
        }
        GameManager.instance.Unpause();
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
