using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void StartGame(int sceneIndex)
    {
        SceneManager.LoadScene(8);
    }
    public void MainMenuQuit()
    {
        Application.Quit();
    }

}
