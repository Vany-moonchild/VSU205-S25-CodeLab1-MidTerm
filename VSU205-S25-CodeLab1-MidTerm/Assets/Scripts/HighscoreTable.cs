using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using System.Linq;

public class HighscoreTable : MonoBehaviour
{
    public static HighscoreTable instance;
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    public TextMeshProUGUI scoreText; // UI Text for score display
    public int currentScore = 0; // Current score in the game
    private string filePath;

    public GameObject parent;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);

        scoreText.text = currentScore.ToString();

        filePath = Application.dataPath + "/Data/highScores.json";

        if (!File.Exists(filePath))
        {
            SaveHighscores(new Highscores { highscoreEntryList = new List<HighscoreEntry>() });
        }

        Highscores highscores = LoadHighscores();

        highscores.highscoreEntryList = highscores.highscoreEntryList
            .OrderByDescending(entry => entry.score)
            .Take(10)
            .ToList();

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
            Debug.Log(highscoreEntry.name);
        }
    }

    void Start()
    {
        parent.SetActive(false);
    }


    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 30f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString = rank + "TH";
        if (rank == 1) rankString = "1ST";
        else if (rank == 2) rankString = "2ND";
        else if (rank == 3) rankString = "3RD";

        entryTransform.Find("Position").GetComponent<TMP_Text>().text = rankString;
        entryTransform.Find("Score").GetComponent<TMP_Text>().text = highscoreEntry.score.ToString();
        entryTransform.Find("Name").GetComponent<TMP_Text>().text = highscoreEntry.name;

        entryTransform.Find("Background").gameObject.SetActive(rank % 2 == 1);

        if (rank == 1)
        {
            entryTransform.Find("Position").GetComponent<TMP_Text>().color = Color.green;
            entryTransform.Find("Name").GetComponent<TMP_Text>().color = Color.green;
            entryTransform.Find("Score").GetComponent<TMP_Text>().color = Color.green;
        }

        transformList.Add(entryTransform);
        
    }

    public void UpdateCurrentScore(int amount)
    {

        currentScore += amount;
        scoreText.text = currentScore.ToString();
        Debug.Log("currentScore" + currentScore);
        
        //Debug.Log(currentScore + " / " + highscoreEntryTransformList.Count);
    }

    public void AddHighScoreEntry(int score, string name)
    {
 
        
        Debug.Log("Adding highscore entry");
        Highscores highscores = LoadHighscores();
        Debug.Log("AddHighScoreEntry: "+score + " " + name);
        highscores.highscoreEntryList.Add(new HighscoreEntry { score = score, name = name });
        SaveHighscores(highscores);
    }

    private Highscores LoadHighscores()
    {
        if (!File.Exists(filePath))
        {
            return new Highscores { highscoreEntryList = new List<HighscoreEntry>() };
        }

        string json = File.ReadAllText(filePath);
        Debug.Log("Highscores LoadHighscores:" + json);
        return JsonUtility.FromJson<Highscores>(json) ?? new Highscores { highscoreEntryList = new List<HighscoreEntry>() };
    }

    private void SaveHighscores(Highscores highscores)
    {
        if (!Directory.Exists(Path.GetDirectoryName(filePath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        }

        Debug.Log("SaveHighscores is being called:" + highscores.highscoreEntryList.Count);
        string json = JsonUtility.ToJson(highscores, true);
        File.WriteAllText(filePath, json);

        ReloadHighscores();
    }

    public void ReloadHighscores()
    {
        LoadHighscores();
        // Debug.Log("SaveHighscores:" + json);
    }
    

    [System.Serializable]
    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }
}
