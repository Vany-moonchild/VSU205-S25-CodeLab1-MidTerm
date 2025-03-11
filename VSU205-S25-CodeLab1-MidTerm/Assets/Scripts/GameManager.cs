using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public HighscoreTable highscoreTable; // Reference to HighscoreTable
    public int currentScore = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        highscoreTable = HighscoreTable.instance; // Access HighscoreTable instance
    }

    private void Start()
    {
        // Initialize the score display from HighscoreTable
        highscoreTable.scoreText.text = currentScore.ToString();
    }

    public void UpdateScore(int scoreAmount)
    {
        currentScore += scoreAmount;
        highscoreTable.UpdateCurrentScore(scoreAmount);
    }

    public void GameOver()
    {
        // When the game ends, add the score to the highscore table
        string playerName = "Player"; // Get player name, maybe from a UI input
        highscoreTable.AddHighScoreEntry(currentScore, playerName);

        // Reset the score after game over
        currentScore = 0;
    }
}