using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndlessGameManager : MonoBehaviour
{

    protected bool gameOver = false;
    protected bool timeStopped = false;
    protected bool soundsMuted = false;

    public AudioSource backgroundMusic;
    public AudioSource defeatSound;

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
            if (EndlessPlayerManager.instance.GetLives() <= 0)
            {
                GameOver();
            }
        }
        if (EndlessPlayerManager.instance.GetLives() <= 0 && !gameOver)
        {
            GameOver();
        }
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
            EndlessUIManager.instance.ShowPause(true);
        }
        else
        {
            StopTime(false);
            EndlessUIManager.instance.ShowPause(false);
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

    protected void PauseSounds(bool pause)
    {
        AudioListener.pause = pause;
    }

    protected void StopTime(bool stop)
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

    protected void MuteSounds(bool mute)
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
    
    public void GameOver()
    {
        ScoreManager.instance.AddScore((int)(PlayerManager.instance.GetMoney() * 0.3));
        ScoreManager.instance.SaveEndlessIntoJson(SceneManager.GetActiveScene().name);
        Time.timeScale = 0;
        backgroundMusic.Pause();
        gameOver = true;
        EndlessUIManager.instance.ShowGameOver(true);
        EndlessBuildManager.instance.Disable();
        defeatSound.Play();
    }

    public void ResetGame()
    {
        PauseSounds(false);
        MuteSounds(false);
        StopTime(false);
        ScoreManager.instance.setScore(0);
        backgroundMusic.Stop();
        EndlessUIManager.instance.ShowPause(false);
        GetComponent<EndlessWaveManager>().ResetSpawner();
        EndlessBuildManager.instance.ResetBuilder();
        EndlessPlayerManager.instance.ResetPlayer();
    }

    public void ChangeLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Scenes/EndlessLevelsMap", LoadSceneMode.Single);
    }
}
