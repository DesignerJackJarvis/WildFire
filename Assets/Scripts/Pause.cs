using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool cantPause;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        if (cantPause) return;
        Time.timeScale = Time.timeScale != 1 ? 1 : 0;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        var audioListener = FindObjectOfType<AudioListener>(true);
        audioListener.enabled = !audioListener.enabled;
    }
    
    public void ResumeGame()
    {
        TogglePause();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Randomised Levels");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
