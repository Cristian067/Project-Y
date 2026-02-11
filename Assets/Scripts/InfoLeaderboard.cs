using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoLeaderboard : MonoBehaviour
{

    private int userPostiion;
    private string username;
    private int userScore;



    [SerializeField] private TextMeshProUGUI positionText;
    [SerializeField] private TextMeshProUGUI usernameText;
    [SerializeField] private TextMeshProUGUI scoreText;



    public void SendInfo(int position,string name, int score)
    {
        userPostiion = position;
        username = name;
        userScore = score;

        
        positionText.text = "#"+userPostiion.ToString();
        usernameText.text = username;
        scoreText.text = userScore.ToString();


    }
}
