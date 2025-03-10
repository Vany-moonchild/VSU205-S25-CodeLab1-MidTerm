using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;
using System;
using Unity.VisualScripting;


public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    //private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");
        

        
        entryTemplate.gameObject.SetActive(false);
        
        
        //initializing the list
        /*highscoreEntryList = new List<HighscoreEntry>()
        {
            new HighscoreEntry { score = 520870, name = "CAT" },
            new HighscoreEntry { score = 980900, name = "JOE" },
            new HighscoreEntry { score = 20010, name = "HEY" },
            new HighscoreEntry { score = 5200, name = "AAA" },
            new HighscoreEntry { score = 1000, name = "BBB" },
            new HighscoreEntry { score = 9200, name = "VAN" },
            new HighscoreEntry { score = 70200, name = "LAB" },
            new HighscoreEntry { score = 53900, name = "KAN" }
        }; */
        
        //get the list from what is saved on the Json
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        
        //Sort entry list by score
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    //Swap
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }
        
        
        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
        
        
        /*
       //so we can use Json
       Highscores highscores = new Highscores { highscoreEntryList = highscoreEntryList };
        
       string json = JsonUtility.ToJson(highscores);
       PlayerPrefs.SetString("highscoreTable", json);
       PlayerPrefs.Save();
       Debug.Log(PlayerPrefs.GetString("highscoreTable"));
       */
       
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container,
        List<Transform> transformList)
    {
        float templateHeight = 30f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH";
                break;
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }



        entryTransform.Find("Position").GetComponent<TMP_Text>().text = rankString;
            
        int score = highscoreEntry.score;
            
        entryTransform.Find("Score").GetComponent<TMP_Text>().text = score.ToString();
            
            
        string name = highscoreEntry.name;
        entryTransform.Find("Name").GetComponent<TMP_Text>().text = name;
        
        //set background visible odds and evens, easier to read 
        entryTransform.Find("Background").gameObject.SetActive(rank % 2 == 1);
        
        //highlight the first entry 
        if (rank == 1)
        {
            entryTransform.Find("Position").GetComponent<TMP_Text>().color = Color.green;
            entryTransform.Find("Name").GetComponent<TMP_Text>().color = Color.green;
            entryTransform.Find("Score").GetComponent<TMP_Text>().color = Color.green;
            
        }
        
        //Set trophy 
        switch (rank)
        {
            default:
                entryTransform.Find("Star").gameObject.SetActive(false);
                break;
            case 1:
                entryTransform.Find("Star").GetComponent<Image>().color = UtilsClass.GetColorFromString("FFD200");
                break;
            case 2:
                entryTransform.Find("Star").GetComponent<Image>().color = UtilsClass.GetColorFromString("C6C6C6");
                break;
            case 3:
                entryTransform.Find("Star").GetComponent<Image>().color = UtilsClass.GetColorFromString("B76F56");
                break;
            
        }
        
        
        transformList.Add(entryTransform);
        
        
        
    }

    private void AddHighScoreEntry(int score, string name)
    {
        //create HighscoreEntry 
        HighscoreEntry highscoreEntry = new HighscoreEntry{score = score, name = name };

    //Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        //Check if list exists
        if (highscores == null)
        {
            highscores = new Highscores();
        }
        if (highscores.highscoreEntryList == null)
        {
            highscores.highscoreEntryList = new List<HighscoreEntry>();
        }
        
        //Add new entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);
        
        //Save Updated
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
        
    }
    
    //object to save 
    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }
    
    
    
   /*
    * Represents a single high score entry 
    */

   [System.Serializable]
   private class HighscoreEntry
   {
       public int score;
       public string name;
   }
    
    
}
