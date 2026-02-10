using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoLeaderboard : MonoBehaviour
{

    public int userPostiion;
    public string username;
    public int userScore;



    [SerializeField] private TextMeshProUGUI positionText;
    [SerializeField] private TextMeshProUGUI usernameText;
    [SerializeField] private TextMeshProUGUI scoreText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void SendInfo(int position,string name, int score)
    {
        userPostiion = position;
        username = name;
        userScore = score;

        
        positionText.text = "#"+position.ToString();
        usernameText.text = username;
        scoreText.text = score.ToString();


    }
}
