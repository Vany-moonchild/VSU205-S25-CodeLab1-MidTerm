using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer instance; 

    public float timeRemaining = 30; // Set the game duration
    
    public bool isTimerRunning = false; 

    public TextMeshProUGUI timerText; // Assign this in the Unity Inspector

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                timerText.text = "00:00";
                isTimerRunning = false;
                GameManager.instance.GameOver();
                Debug.Log(" time remaining is over");

            }
            else
            {
                
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI();
            }
        }
    }

    public void StartTimer()
    {
        isTimerRunning = true;
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
        
        
    }

    private void EndGame()
    {
        GameManager.instance.GameOver();
        
        // if (HighscoreTable.instance != null)
        // {
        //     string playerName = PlayerPrefs.GetString("PlayerName", "Unknown");
        //     int finalScore = HighscoreTable.instance.currentScore;
        //     HighscoreTable.instance.AddHighScoreEntry(finalScore, playerName);
        // }
    }
}