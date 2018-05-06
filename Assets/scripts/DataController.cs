using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;
public class DataController : MonoBehaviour
{
    private RoundData[] allRoundData;
    private PlayerProgress PlayerProgress;
    private string gameDataFileName = "data.json";


    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        LoadGameData();
        LoadPlayerProgress();
        //SceneManager.LoadScene("MenuScreen");
    }

    public RoundData GetCurrentRoundData()
    {
        return allRoundData [0];
    }

    public void SubmitNewPlayerScore(int newScore)
    {
        if (newScore > PlayerProgress.highestScore)
        {
            PlayerProgress.highestScore = newScore;
            SavePlayerProgress();
        }
    }
    public int GetHighestPlayerScore()
    {
        return PlayerProgress.highestScore;
    }
    private void LoadPlayerProgress()
    {
        PlayerProgress = new PlayerProgress();

        if (PlayerPrefs.HasKey("highestScore"))
        {
            PlayerProgress.highestScore = PlayerPrefs.GetInt("highestScore");
        }
    }
    private void SavePlayerProgress()
    {
        PlayerPrefs.SetInt("highestScore", PlayerProgress.highestScore);
    }
    private void LoadGameData()
    {   

        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            GameData loadData = JsonUtility.FromJson<GameData>(dataAsJson);
            allRoundData = loadData.allRoundData;
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
            
    }
}
