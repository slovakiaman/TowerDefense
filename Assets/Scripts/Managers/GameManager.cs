using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool gameOver = false;
    private bool timeStopped = false;
    private bool soundsMuted = false;

    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource victorySound;
    [SerializeField] private AudioSource defeatSound;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleTime();
        }
        if (!gameOver)
        {
            if (PlayerManager.instance.GetLives() <= 0)
            {
                GameOver();
            } else if (PlayerManager.instance.GetVictory())
            {
                Victory();
            }
        }
        if (PlayerManager.instance.GetLives() <= 0 && !gameOver)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        ScoreManager.instance.setScore(0);
        Time.timeScale = 0;
        backgroundMusic.Pause();
        gameOver = true;
        UIManager.instance.ShowGameOver(true);
        BuildManager.instance.Disable();
        defeatSound.Play();
    }

    private void Victory()
    {
        ScoreManager.instance.SaveIntoJson(SceneManager.GetActiveScene().name);
        Time.timeScale = 0;
        backgroundMusic.Pause();
        gameOver = true;
        UIManager.instance.ShowVictory(true);
        BuildManager.instance.Disable();
        victorySound.Play();
    }

    public void ToggleTime()
    {
        if (gameOver)
            return;

        if (!timeStopped)
        {
            UIManager.instance.ShowPause(true);
            Time.timeScale = 0;
            backgroundMusic.Pause();
        }
        else
        {
            UIManager.instance.ShowPause(false);
            backgroundMusic.UnPause();
            Time.timeScale = 1;
        }
        timeStopped = !timeStopped;
    }

    public void ToggleSounds()
    {
        if (!soundsMuted)
            AudioListener.volume = 0;
        else
            AudioListener.volume = 1;
        soundsMuted = !soundsMuted;
    }

    public void PlayBackgroundMusic()
    {
        backgroundMusic.Play();
    }

    public void ResetGame()
    {
        ScoreManager.instance.setScore(0);
        backgroundMusic.Stop();
        UIManager.instance.ShowPause(false);
        timeStopped = false;
        Time.timeScale = 1;
        gameOver = false;
        GetComponent<WaveManager>().ResetSpawner();
        BuildManager.instance.ResetBuilder();
        PlayerManager.instance.ResetPlayer();
    }

    public void ChangeLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Scenes/LevelsMap", LoadSceneMode.Single);
    }

    public void Menu()
    {
        ScoreManager.instance.setScore(0);
        Time.timeScale = 1;
        SceneManager.LoadScene("Scenes/MainMenuScene", LoadSceneMode.Single);
    }

    public void NextLevel()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(++buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
