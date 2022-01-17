using EndlessMode.Enemies;

namespace EndlessMode.Managers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using EndlessMode.Waves;
    using UnityEngine;

    public class WaveManager : MonoBehaviour
    {
        public float countdown;
        public int waveNumber = 0;
        protected bool spawning = false;
        protected bool start = false;
        
        public List<Wave> waves;
        public int nextWaveNumber;
        public int wavesCompleted;
        public float Coeficient = 1.3f;

        public bool currentWaveEventGenerated = false;

        void Start()
        {
            countdown = this.waves[waveNumber].countdown;
            nextWaveNumber = 0;
            UIManager.instance.ShowNextWaveEnemy(waves[0].GetEnemyPrefab().GetComponent<Enemy>().variant, true);
        }

        void Update()
        {
            if (start)
            {
                if (!spawning)
                {
                    UIManager.instance.ShowNextWaveCountdown(Math.Round(countdown));
                    if (countdown <= 0f)
                    {
                        if (!currentWaveEventGenerated && nextWaveNumber > 0)
                        {
                            EventManager.instance.GenerateWaveEvent(waveNumber);
                            currentWaveEventGenerated = true;
                            countdown = 4f;
                        }
                        else
                        {
                            UIManager.instance.HideEventPanel();
                            StartCoroutine(SpawnWave());
                            currentWaveEventGenerated = false;
                        }
                    }
                    countdown -= ValueCalculator.instance.CalculateTimerDeltaTime(Time.deltaTime);
                }
                else
                {
                    UIManager.instance.ShowWaveSpawning(waveNumber + 1);
                }
            }
            else
            {
                UIManager.instance.ShowGameNotStartedText();
            }
        }

        public void SpawnAnotherWave()
        {
            countdown = 0;
            UIManager.instance.ShowSpawnWaveButton(false);
            PlayerManager.instance.AddMoney(200);
        }

        public IEnumerator SpawnWave()
        {
            waveNumber = nextWaveNumber;
            if (wavesCompleted >= waves.Count - 1)
            {
                nextWaveNumber = UnityEngine.Random.Range(0, this.waves.Count);
            } else
            {
                nextWaveNumber = waveNumber + 1;
            }
            
            spawning = true;

            Debug.Log("Spawning wave " + (wavesCompleted + 1) + "!");
            
            UIManager.instance.ShowSpawnWaveButton(false);

            Wave wave = this.waves[waveNumber];
            int assignedPath = ((Waypoints)Waypoints.instance).AssignPath(wave.waveDifficulty);
            wave.SetAssignedPathIndex(assignedPath);
            Transform spawnPoint = ((Waypoints)Waypoints.instance).GetNextWaypoint(wave.GetAssignedPathIndex(), -1);
            
            //wave calculated values
            int noEnemies = ValueCalculator.instance.CalculateEnemyCount(wave);
            float enemyHp = ValueCalculator.instance.CalculateEnemyHP(wave);
            float enemySpeed = ValueCalculator.instance.CalculateEnemySpeed(wave);
            int enemyMoneyReward = ValueCalculator.instance.CalculateEnemyMoneyReward(wave);
            int enemyScoreReward = wave.moneyReward;
            for (int i = 0; i < noEnemies; i++)
            {
                GameObject enemyObject = Instantiate(wave.GetEnemyPrefab(), spawnPoint.position, wave.GetEnemyPrefab().transform.rotation);
                
                if (wavesCompleted >= waves.Count)
                {
                    float multiplier = (int)(wavesCompleted / waves.Count);
                    float multiply = Mathf.Pow(Coeficient, multiplier);
                    enemyHp = enemyHp * multiply;
                }
                enemyObject.GetComponent<Enemy>().SetupValues(wave.GetAssignedPathIndex(), enemySpeed, enemyHp, enemyMoneyReward, enemyScoreReward, wave.scale);
                yield return new WaitForSeconds(1.7f - (enemySpeed * 0.15f));
            }
            if (waveNumber < waves.Count)
                countdown = this.waves[waveNumber].countdown;
            spawning = false;
            wavesCompleted++;
            UIManager.instance.ShowNextWaveEnemy(waves[waveNumber].GetEnemyPrefab().GetComponent<Enemy>().variant, false);
            UIManager.instance.ShowNextWaveEnemy(waves[nextWaveNumber].GetEnemyPrefab().GetComponent<Enemy>().variant, true);
            UIManager.instance.ShowSpawnWaveButton(true);
        }

        public void StartSpawner()
        {
            this.start = true;
        }

        public void ResetSpawner()
        {
            UIManager.instance.ShowStartButton(true);
            UIManager.instance.ShowSpawnWaveButton(false);
            StopAllCoroutines();

            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemyObject in gameObjects)
            {
                Destroy(enemyObject);
            }
            if (spawning)
                UIManager.instance.ShowNextWaveEnemy(waves[nextWaveNumber].GetEnemyPrefab().GetComponent<Enemy>().variant, false);
            else
                UIManager.instance.ShowNextWaveEnemy(waves[waveNumber].GetEnemyPrefab().GetComponent<Enemy>().variant, false); ;
            spawning = false;
            start = false;
            waveNumber = 0;
            nextWaveNumber = 0;
            wavesCompleted = 0;
            countdown = this.waves[waveNumber].countdown;
            UIManager.instance.ShowNextWaveEnemy(waves[0].GetEnemyPrefab().GetComponent<Enemy>().variant, true);
            UIManager.instance.ShowGameNotStartedText();
        }
    }
}