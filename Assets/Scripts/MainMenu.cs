using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StoryMode()
    {
        SceneManager.LoadScene("Scenes/LevelsMap", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
