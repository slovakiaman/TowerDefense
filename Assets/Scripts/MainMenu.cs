using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject endlessButton;
    [SerializeField]
    private string lastLevelName;
    private void Awake()
    {
        Time.timeScale = 1;
        AudioListener.volume = 1;
        
    }
    private void Start()
    {
        ulong score = 0;
        bool is_exist = ScoreManager.instance.getScoreByLevel(lastLevelName, out score);
        if (!is_exist)
        {
            //endlessButton.GetComponent<Button>().interactable = false;
        }
    }
    public void StoryMode()
    {
        SceneManager.LoadScene("Scenes/LevelsMap", LoadSceneMode.Single);
    }
    public void EndlessMode()
    {
        SceneManager.LoadScene("Scenes/EndlessLevelsMap", LoadSceneMode.Single);
    }

    public void Credits()
    {
        SceneManager.LoadScene("Scenes/Credits", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
