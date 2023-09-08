using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    private int score = 0;

    public void UpdateScore()
    {
        scoreText.text = "Score: " + ++score;
    }
}
