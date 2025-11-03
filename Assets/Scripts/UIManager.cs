using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager instance { get; private set;}
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI totalPointsText;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

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
    }
    

}
