using System;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField NameInput;
    public Button StartButton;
    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI TargetsText;

    // Start is called before the first frame update
    void Start()
    {
        NameInput.Select();
        HighScoreText.text = $"MISSION:{Environment.NewLine}BEAT {HighScoreManager.HighScore.PlayerName}'S {HighScoreManager.HighScore.Score}";
        TargetsText.text = GetTargetsText();
    }

    private string GetTargetsText()
    {
        StringBuilder sb = new();

        int counter = 1;
        foreach (var target in HighScoreManager.HighScores.OrderByDescending(s => s.Score))
        {
            sb.AppendLine($"{counter++}. {target.PlayerName}'S {target.Score}");
        }

        return sb.ToString();
    }

    public void OnNameChanged(string name)
    {
        StartButton.interactable = !string.IsNullOrEmpty(name);
    }

    public void StartGame()
    {
        PlayerNameManager.SetPlayerName(NameInput.text);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
