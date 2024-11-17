using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public string SceneName;
    private bool pauseBool;

    void Start()
    {
        pauseMenu.SetActive(false);
        pauseBool = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneName);
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);        
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void HomeGame()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void WinGame()
    {

    }

    public void ExitGame()
    {

    }
}
