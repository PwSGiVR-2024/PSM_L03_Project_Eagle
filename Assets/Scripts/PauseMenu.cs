using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameStateManager;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuUI;
    private bool isPaused = false;
    private GameState gameState;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isPaused)
            {
                resume();
            }
            else
            {
                pause();
            }
        }
    }

    public void pause()
    {
        gameState = GameStateManager.Instance.CurrentState;
        GameStateManager.Instance.SetState(GameState.Pause);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void resume()
    {
        GameStateManager.Instance.SetState(gameState);
        pauseMenuUI?.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LoadMainMenu(int sceneIndex)
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        Application.Quit();
    }
}
