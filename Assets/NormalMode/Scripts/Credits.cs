using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1;
        AudioListener.volume = 1;

    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenuScene", LoadSceneMode.Single);
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            this.MainMenu();
        }   
    }
}
