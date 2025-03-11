using System;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        GameObject gameManager = GameObject.Find("GameManager");

        // Reset the player's score
        HighscoreTable.instance.UpdateCurrentScore(-HighscoreTable.instance.currentScore);

        // Reload the level (assuming it doesn't reset the timer)
        gameManager.GetComponent<ASCIILevelLoader>().LoadLevel();
    }
}