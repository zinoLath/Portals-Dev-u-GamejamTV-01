using UnityEngine;
//using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    
    private void Start()
    {
        PauseMenuUI.SetActive(false);
    }

    void OnPause (bool value)// not working, for test purposes
    {
        if (value)
        {
            if (GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    private void PauseGame()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.doYouOnPause(GameIsPaused);
        }
    }

    public void ResumeGame()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.doYouOnPause(GameIsPaused);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("ByeBye");
    }
}