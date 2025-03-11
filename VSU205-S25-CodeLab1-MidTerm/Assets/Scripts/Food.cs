using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private int scoreIncrease = 10; // Points gained when food is eaten
    [SerializeField] private float spawnRange = 5f; // Adjust spawn range as needed

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision with: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is the one that collided with Food");
            
            scoreIncrease += 10;
            
            Debug.Log("Score: " + scoreIncrease);
            
            if (HighscoreTable.instance != null)
            {
                Debug.Log("OnCollisionEnter with Food, instance found" + scoreIncrease);
                
                
                
                HighscoreTable.instance.currentScore = scoreIncrease;
                //HighscoreTable.instance.scoreText.text = HighscoreTable.instance.currentScore.ToString();
                //Debug.Log("Score: " + HighscoreTable.instance.currentScore);
                
            }

            RespawnFood();
        }

        
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