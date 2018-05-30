using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;                                                        // The System.IO namespace contains functions related to loading and saving files
using System;

public class DataController2 : MonoBehaviour
{
    private RoundData[] allRoundData;
    private PlayerProgress playerProgress;
    Scene m_scene;
    private string gameDataFileName = "data3.txt";

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        LoadGameData();
        LoadPlayerProgress();
    }

    public RoundData GetCurrentRoundData()
    {
        // If we wanted to return different rounds, we could do that here
        // We could store an int representing the current round index in PlayerProgress

        return allRoundData[playerProgress.currentRound];
    }

    public void SubmitNewPlayerScore(int newScore)
    {
        // If newScore is greater than playerProgress.highestScore, update playerProgress with the new value and call SavePlayerProgress()
        if (newScore > playerProgress.highestScore)
        {
            playerProgress.highestScore = newScore;
            SavePlayerProgress();
        }
    }

    public int GetHighestPlayerScore()
    {
        return playerProgress.highestScore;
    }

    private void LoadGameData()
    {
        // Path.Combine combines strings into a file path
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        string filePath = "";
        Debug.Log("[9onmay] Trying to find file " + filePath);

#if UNITY_ANDROID
        filePath = "jar:file://" + Application.dataPath + "!/assets/" + gameDataFileName;
        Debug.Log("[9onmay] ON ANDROID: Trying to find file " + filePath);
#else
        filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);
#endif
        
        // Get the file content by using the WWW object (it handles the Android file-system correctly)
        WWW wwwfile = new WWW(filePath);
        while (!wwwfile.isDone) { }

        try
        {
            // Read the json from the file into a string
            string dataAsJson = wwwfile.text;

            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);

            // Retrieve the allRoundData property of loadedData
            allRoundData = loadedData.allRoundData;
        }
        catch (Exception e)
        {
            Debug.Log("[9onMay] Failed to read file " + filePath + ". Exception: " + e);

            throw;
        }
    }

    // This function could be extended easily to handle any additional data we wanted to store in our PlayerProgress object
    private void LoadPlayerProgress()
    {
        // Create a new PlayerProgress object
        playerProgress = new PlayerProgress();

        // If PlayerPrefs contains a key called "highestScore", set the value of playerProgress.highestScore using the value associated with that key
        if (PlayerPrefs.HasKey("highestScore"))
        {
            playerProgress.highestScore = PlayerPrefs.GetInt("highestScore");
        }
    }

    // This function could be extended easily to handle any additional data we wanted to store in our PlayerProgress object
    private void SavePlayerProgress()
    {
        // Save the value playerProgress.highestScore to PlayerPrefs, with a key of "highestScore"
        PlayerPrefs.SetInt("highestScore", playerProgress.highestScore);
    }
}