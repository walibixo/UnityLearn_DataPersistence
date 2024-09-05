using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField NameInput;
    public Button StartButton;

    // Start is called before the first frame update
    void Start()
    {
        NameInput.Select();
    }

    // Update is called once per frame
    void Update()
    {

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
