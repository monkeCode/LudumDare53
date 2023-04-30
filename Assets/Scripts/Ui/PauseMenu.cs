using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void Close()
    {
        PauseManager.Instance.PauseOff();
        gameObject.SetActive(false);
    }

    public void ToMainMenu()
    {
        PauseManager.Instance.PauseOff();
        SceneManager.LoadScene("MainMenu");
    }
}
