namespace NormalMode.Managers
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class GameManager : MonoBehaviour
    {
        private bool gameOver = false;
        private bool timeStopped = false;
        private bool soundsMuted = false;

        public AudioSource backgroundMusic;
        public AudioSource victorySound;
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
            PauseSounds(false);
            MuteSounds(false);
            StopTime(false);
            SceneManager.LoadScene("Scenes/MainMenuScene", LoadSceneMode.Single);
        }

        public void NextLevel()
        {
            PauseSounds(false);
            MuteSounds(false);
            StopTime(false);
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(++buildIndex);
        }

        public void Quit()
        {
            Application.Quit();
        }
        
        public void GameOver()
        {
            ScoreManager.instance.setScore(0);
            Time.timeScale = 0;
            backgroundMusic.Pause();
            gameOver = true;
            UIManager.instance.ShowGameOver(true);
            BuildManager.instance.Disable();
            defeatSound.Play();
        }

        public void Victory()
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

        public void ResetGame()
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

        public void ChangeLevel()
        {
            PauseSounds(false);
            MuteSounds(false);
            StopTime(false);
            SceneManager.LoadScene("Scenes/LevelsMap", LoadSceneMode.Single);
        }

    }
       
}