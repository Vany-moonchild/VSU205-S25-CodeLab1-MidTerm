using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer instance; 

    public float timeRemaining = 60f; // Set the game duration
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
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI();
            }
            else
            {
                timeRemaining = 0;
                isTimerRunning = false;
                EndGame();
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
        if (HighscoreTable.instance != null)
        {
            string playerName = PlayerPrefs.GetString("PlayerName", "Unknown");
            int finalScore = HighscoreTable.instance.currentScore;
            HighscoreTable.instance.AddHighScoreEntry(finalScore, playerName);
        }
    }
}