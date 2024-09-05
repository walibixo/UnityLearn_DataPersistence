using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    public static HighScoresData.HighScoreData HighScore { get; private set; }
    public static List<HighScoresData.HighScoreData> HighScores { get; private set; }

    public static void SetHighScore(int score)
    {
        HighScores.Add(new HighScoresData.HighScoreData
        {
            Score = score,
            PlayerName = PlayerNameManager.PlayerName
        });

        if (HighScores.Count > 6)
        {
            HighScores = HighScores.OrderByDescending(s => s.Score).Take(6).ToList();
        }

        HighScore = HighScores.OrderByDescending(s => s.Score).First();

        SaveHighScore();
    }

    private static void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        HighScoresData data = null;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<HighScoresData>(json);
        }

        HighScore = data?.HighScores.OrderByDescending(s => s.Score).FirstOrDefault() ?? new()
        {
            Score = 0,
            PlayerName = "NULL"
        };

        HighScores = data?.HighScores ?? new List<HighScoresData.HighScoreData>() {
            HighScore,
            HighScore,
            HighScore,
            HighScore,
            HighScore,
            HighScore
        };
    }

    private static void SaveHighScore()
    {
        string json = JsonUtility.ToJson(new HighScoresData(HighScores));

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    [Serializable]
    public class HighScoresData
    {
        public List<HighScoreData> HighScores;

        public HighScoresData()
        {
            HighScores = new List<HighScoreData>();
        }

        public HighScoresData(List<HighScoreData> highScores)
        {
            HighScores = highScores;
        }

        [Serializable]
        public class HighScoreData
        {
            public int Score;
            public string PlayerName;
        }
    }
}
