using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public HighscoreTable highscoreTable; // Reference to HighscoreTable

    private string input;
    
    public ReadInput readInput;

    public string playerName;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        // Initialize the score display from HighscoreTable
       // highscoreTable.scoreText.text = "Score:"; //;+ highscoreTable.scoreIncrease;



    }

    public void GetInfo()
    {
        // Reference ReadInput and get the player's name
        if (readInput != null)
        {
            playerName = readInput.GetInputName();
            Debug.Log("Retrieved Player Name: " + playerName);
            
        }
        else
        {
            Debug.LogWarning("ReadInput script not found in the scene!");
        }
        
        
    }



    public void GameOver()
    {
        Debug.Log("Game Over");
        // When the game ends, add the score to the highscore table
        GetInfo();
        Debug.Log("Got the playerName:" + HighscoreTable.instance.gameObject.name);
        HighscoreTable.instance.AddHighScoreEntry(HighscoreTable.instance.currentScore, playerName);

        Debug.Log(HighscoreTable.instance.currentScore + " : " + playerName);
        //Debug.Log(AddHighScoreEntry(highscoreTable.currentScore, playerName));
        // Reset the score after game over
        //highscoreTable.currentScore = 0;
    }
    
    
    
}