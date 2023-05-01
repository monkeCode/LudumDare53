using System;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public void Restart()
    {
        var currentScene = SceneManager.GetActiveScene();
        PauseManager.Instance.canOffPause = true;
        PauseManager.Instance.PauseOff();
        SceneManager.LoadScene(currentScene.name);
    }

    public void ToMainMenu()
    {
        PauseManager.Instance.canOffPause = true;
        PauseManager.Instance.PauseOff();
        SceneManager.LoadScene("MainMenu");
    }
}