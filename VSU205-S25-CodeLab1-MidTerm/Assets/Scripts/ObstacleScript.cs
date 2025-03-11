using System;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        GameObject gameManager = GameObject.Find("GameManager");

        // Reset the player's score
        // HighscoreTable.instance.currentScore = 0;
        Debug.Log("Reset Score to 0");

        // Reload the level (assuming it doesn't reset the timer)
        gameManager.GetComponent<ASCIILevelLoader>().LoadLevel();
    }
}