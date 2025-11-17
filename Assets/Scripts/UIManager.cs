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

    [SerializeField] private Slider upgradeProgressSlider;


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
        

        //RefreshStatsUi();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RefreshStatsUi()
    {
        livesText.text = $"{GameManager.instance.GetPlayerLives()}/6";
        totalPointsText.text = $"{GameManager.instance.GetTotalPoints()}";

        upgradeProgressSlider.value = GameManager.instance.GetPoints();
        upgradeProgressSlider.maxValue = GameManager.instance.GetPointsToUpgrade();
    }
    

}
