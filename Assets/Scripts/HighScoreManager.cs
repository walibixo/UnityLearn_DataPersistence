using System;
using System.IO;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadHighScore();
    }

    public static HighScoreData HighScore { get; private set; }

    public static void SetHighScore(int score)
    {
        if (score > HighScore.Score)
        {
            HighScore.Score = score;
            HighScore.PlayerName = PlayerNameManager.PlayerName;
        }
    }

    private void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        HighScoreData data = null;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<HighScoreData>(json);
        }

        HighScore = data ?? new()
        {
            Score = 0,
            PlayerName = "NULL"
        };
    }

    private void SaveHighScore()
    {
        string json = JsonUtility.ToJson(HighScore);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    [Serializable]
    public class HighScoreData
    {
        public int Score;
        public string PlayerName;
    }
}
