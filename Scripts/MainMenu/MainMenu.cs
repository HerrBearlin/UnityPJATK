using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene(3);
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
