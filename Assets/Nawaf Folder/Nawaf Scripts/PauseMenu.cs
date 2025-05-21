using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject settingsPanel;

    public MonoBehaviour playerController;     
    public MonoBehaviour cameraController;     

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
    }

    public void ExitGame()
    {
        
        Application.Quit();
    }
}
