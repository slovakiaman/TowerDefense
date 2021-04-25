using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1;
        AudioListener.volume = 1;
    }
    public void StoryMode()
    {
        SceneManager.LoadScene("Scenes/LevelsMap", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
