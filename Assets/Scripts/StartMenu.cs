using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public Text highScoreText;

    void Start()
    {
        if(PlayerPrefs.GetString("HIGHSCORENAME") != string.Empty)
        {
            highScoreText.text = "High Score:\n" + PlayerPrefs.GetInt("HIGHSCORE") + " - " + PlayerPrefs.GetString("HIGHSCORENAME");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Button Pushed");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }
}
