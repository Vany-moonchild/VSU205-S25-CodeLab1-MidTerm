using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private int scoreIncrease = 10; // Points gained when food is eaten
    [SerializeField] private float spawnRange = 5f; // Adjust spawn range as needed

    private void OnCollisionEnter(Collision other)
    {
     if (HighscoreTable.instance != null)
            {
                HighscoreTable.instance.UpdateCurrentScore(scoreIncrease);
            }

            RespawnFood();
        
    }

    private void RespawnFood()
    {
        transform.position = new Vector3(
            Random.Range(-spawnRange, spawnRange),
            Random.Range(-spawnRange, spawnRange),
            transform.position.z // Keeping Z position the same
        );
    }
}