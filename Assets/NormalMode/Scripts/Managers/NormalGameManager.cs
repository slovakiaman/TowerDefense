using UnityEngine;
using UnityEngine.SceneManagement;

public class NormalGameManager : GameManager
{
    public override void GameOver()
    {
        ScoreManager.instance.setScore(0);
        Time.timeScale = 0;
        backgroundMusic.Pause();
        gameOver = true;
        UIManager.instance.ShowGameOver(true);
        BuildManager.instance.Disable();
        defeatSound.Play();
    }

    public override void Victory()
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

    public override void ResetGame()
    {
        PauseSounds(false);
        MuteSounds(false);
        StopTime(false);
        gameOver = false;
        ScoreManager.instance.setScore(0);
        backgroundMusic.Stop();
        UIManager.instance.ShowPause(false);
        GetComponent<WaveManager>().ResetSpawner();
        BuildManager.instance.ResetBuilder();
        PlayerManager.instance.ResetPlayer();
        DialogueManager.instance.Reset();
    }

    public override void ChangeLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Scenes/LevelsMap", LoadSceneMode.Single);
    }

}
