using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public enum PreviousPanel { None, Pause, GameOver }
    private PreviousPanel previousPanel = PreviousPanel.None;

    public GameObject pausePanel;
    public GameObject settingsPanel;

    public MonoBehaviour playerController;
    public MonoBehaviour cameraController;
    public GameObject gameOverPanel;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsPanel.activeSelf)
            {
                settingsPanel.SetActive(false);
                return;
            }

            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;

        if (playerController != null)
            playerController.enabled = !isPaused;

        if (cameraController != null)
            cameraController.enabled = !isPaused;

        // Lock or unlock cursor
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;

        if (playerController != null)
            playerController.enabled = true;

        if (cameraController != null)
            cameraController.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);

    if (pausePanel.activeSelf)
    {
        pausePanel.SetActive(false);
        previousPanel = PreviousPanel.Pause;
    }
    else if (gameOverPanel != null && gameOverPanel.activeSelf)
    {
        gameOverPanel.SetActive(false);
        previousPanel = PreviousPanel.GameOver;
    }
    else
    {
        previousPanel = PreviousPanel.None;
    }
    }

    public void CloseSettings()
    {
       settingsPanel.SetActive(false);

    if (previousPanel == PreviousPanel.Pause)
        pausePanel.SetActive(true);
    else if (previousPanel == PreviousPanel.GameOver)
        gameOverPanel.SetActive(true);

    previousPanel = PreviousPanel.None;
    }

    public void ExitGame()
    {

        Application.Quit();
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Optional: pause the game
    }

public void RestartGame()
{
    Time.timeScale = 1f;
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}

public void GoToMainMenu()
{
    Time.timeScale = 1f;
    SceneManager.LoadScene("MainMenu"); // Replace with your main menu scene name
}
}
