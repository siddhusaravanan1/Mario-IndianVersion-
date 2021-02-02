using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public GameObject pausepanel;
    public GameObject pausebutton;


    public void PauseMenuBehaviour()
    {
        pausepanel.SetActive(true);
        pausebutton.SetActive(false);

        Time.timeScale = 0;
    }
    public void ResumeMenu()
    {
        pausepanel.SetActive(false);
        pausebutton.SetActive(true);

        Time.timeScale = 1;
    }
    public void RestartMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

