using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndlessGameManager : GameManager
{

    public override void GameOver()
    {
        ScoreManager.instance.AddScore((int)(PlayerManager.instance.GetMoney() * 0.3));
        ScoreManager.instance.SaveEndlessIntoJson(SceneManager.GetActiveScene().name);
        Time.timeScale = 0;
        backgroundMusic.Pause();
        gameOver = true;
        UIManager.instance.ShowGameOver(true);
        BuildManager.instance.Disable();
        defeatSound.Play();
    }

    public override void Victory()
    {
        //not possible in endless mode
    }

    public override void ResetGame()
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
    }

    public override void ChangeLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Scenes/EndlessLevelsMap", LoadSceneMode.Single);
    }
}
