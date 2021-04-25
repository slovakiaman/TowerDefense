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

    private void Awake()
    {
        PauseSounds(false);
        MuteSounds(false);
        StopTime(false);
    }

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
        ScoreManager.instance.AddScore((int)(PlayerManager.instance.GetMoney() * 0.3));
        ScoreManager.instance.SaveIntoJson(SceneManager.GetActiveScene().name);
        backgroundMusic.Pause();
        DialogueManager.instance.ShowDialogue("end");
        gameOver = true;
        UIManager.instance.ShowVictory(true);
        BuildManager.instance.Disable();
        victorySound.Play();
    }

    public void Pause()
    {
        ToggleTime();
        if (timeStopped)
            PauseSounds(true);
        else
            PauseSounds(false);
    }

    public void ToggleTime()
    {
        if (gameOver)
            return;

        if (!timeStopped)
        {
            StopTime(true);
            UIManager.instance.ShowPause(true);
        }
        else
        {
            StopTime(false);
            UIManager.instance.ShowPause(false);
        }
    }

    public void ToggleSounds()
    {
        if (gameOver)
            return;

        if (!soundsMuted)
            MuteSounds(true);
        else
            MuteSounds(false);
    }

    private void PauseSounds(bool pause)
    {
        AudioListener.pause = pause;
    }

    private void StopTime(bool stop)
    {
        if (stop)
        {
            Time.timeScale = 0;
            timeStopped = true;
        }
        else
        {
            Time.timeScale = 1;
            timeStopped = false;
        }
    }

    private void MuteSounds(bool mute)
    {
        if (mute)
        {
            AudioListener.volume = 0;
            soundsMuted = true;
        }
        else
        {
            AudioListener.volume = 1;
            soundsMuted = false;
        }
    }


    public void PlayBackgroundMusic()
    {
        backgroundMusic.Play();
    }

    public void ResetGame()
    {
        PauseSounds(false);
        MuteSounds(false);
        StopTime(false);
        ScoreManager.instance.setScore(0);
        backgroundMusic.Stop();
        UIManager.instance.ShowPause(false);
        GetComponent<WaveManager>().ResetSpawner();
        BuildManager.instance.ResetBuilder();
        PlayerManager.instance.ResetPlayer();
        DialogueManager.instance.Reset();
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
