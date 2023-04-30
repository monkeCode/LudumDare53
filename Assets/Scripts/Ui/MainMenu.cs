using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
    public class MainMenu : MonoBehaviour
    {
        public string mainSceneName = "monsterScene";
        
        public void StartGame()
        {
            SceneManager.LoadScene(mainSceneName);
        }

        public void QuitGame()
        {
            Debug.Log("Quit");
            Application.Quit();
        }

    }
}
