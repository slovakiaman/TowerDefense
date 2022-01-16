using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class EndlessWaveManager : MonoBehaviour
{
    public float countdown;
    public int waveNumber = 0;
    protected bool spawning = false;
    protected bool start = false;
    
    public List<EndlessWave> waves;
    public int nextWaveNumber;
    public int wavesCompleted;
    public float endlessCoeficient = 1.3f;

    public bool currentWaveEventGenerated = false;

    void Start()
    {
        countdown = this.waves[waveNumber].countdown;
        nextWaveNumber = 0;
        EndlessUIManager.instance.ShowNextWaveEnemy(waves[0].GetEnemyPrefab().GetComponent<EndlessEnemy>().variant, true);
    }

    void Update()
    {
        if (start)
        {
            if (!spawning)
            {
                EndlessUIManager.instance.ShowNextWaveCountdown(Math.Round(countdown));
                if (countdown <= 0f)
                {
                    if (!currentWaveEventGenerated && nextWaveNumber > 0)
                    {
                        EndlessEventManager.instance.GenerateWaveEvent(waveNumber);
                        currentWaveEventGenerated = true;
                        countdown = 4f;
                    }
                    else
                    {
                        EndlessUIManager.instance.HideEventPanel();
                        StartCoroutine(SpawnWave());
                        currentWaveEventGenerated = false;
                    }
                }
                countdown -= EndlessValueCalculator.instance.CalculateTimerDeltaTime(Time.deltaTime);
            }
            else
            {
                EndlessUIManager.instance.ShowWaveSpawning(waveNumber + 1);
            }
        }
        else
        {
            EndlessUIManager.instance.ShowGameNotStartedText();
        }
    }

    public void SpawnAnotherWave()
    {
        countdown = 0;
        EndlessUIManager.instance.ShowSpawnWaveButton(false);
        EndlessPlayerManager.instance.AddMoney(200);
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
        
        EndlessUIManager.instance.ShowSpawnWaveButton(false);

        EndlessWave wave = this.waves[waveNumber];
        int assignedPath = ((EndlessWaypoints)Waypoints.instance).AssignPath(wave.waveDifficulty);
        wave.SetAssignedPathIndex(assignedPath);
        Transform spawnPoint = ((EndlessWaypoints)Waypoints.instance).GetNextWaypoint(wave.GetAssignedPathIndex(), -1);
        
        //wave calculated values
        int noEnemies = EndlessValueCalculator.instance.CalculateEnemyCount(wave);
        float enemyHp = EndlessValueCalculator.instance.CalculateEnemyHP(wave);
        float enemySpeed = EndlessValueCalculator.instance.CalculateEnemySpeed(wave);
        int enemyMoneyReward = EndlessValueCalculator.instance.CalculateEnemyMoneyReward(wave);
        int enemyScoreReward = wave.moneyReward;
        for (int i = 0; i < noEnemies; i++)
        {
            GameObject enemyObject = Instantiate(wave.GetEnemyPrefab(), spawnPoint.position, wave.GetEnemyPrefab().transform.rotation);
            
            if (wavesCompleted >= waves.Count)
            {
                float multiplier = (int)(wavesCompleted / waves.Count);
                float multiply = Mathf.Pow(endlessCoeficient, multiplier);
                enemyHp = enemyHp * multiply;
            }
            enemyObject.GetComponent<EndlessEnemy>().SetupValues(wave.GetAssignedPathIndex(), enemySpeed, enemyHp, enemyMoneyReward, enemyScoreReward, wave.scale);
            yield return new WaitForSeconds(1.7f - (enemySpeed * 0.15f));
        }
        if (waveNumber < waves.Count)
            countdown = this.waves[waveNumber].countdown;
        spawning = false;
        wavesCompleted++;
        EndlessUIManager.instance.ShowNextWaveEnemy(waves[waveNumber].GetEnemyPrefab().GetComponent<EndlessEnemy>().variant, false);
        EndlessUIManager.instance.ShowNextWaveEnemy(waves[nextWaveNumber].GetEnemyPrefab().GetComponent<EndlessEnemy>().variant, true);
        EndlessUIManager.instance.ShowSpawnWaveButton(true);
    }

    public void StartSpawner()
    {
        this.start = true;
    }

    public void ResetSpawner()
    {
        EndlessUIManager.instance.ShowStartButton(true);
        EndlessUIManager.instance.ShowSpawnWaveButton(false);
        StopAllCoroutines();

        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemyObject in gameObjects)
        {
            Destroy(enemyObject);
        }
        if (spawning)
            EndlessUIManager.instance.ShowNextWaveEnemy(waves[nextWaveNumber].GetEnemyPrefab().GetComponent<EndlessEnemy>().variant, false);
        else
            EndlessUIManager.instance.ShowNextWaveEnemy(waves[waveNumber].GetEnemyPrefab().GetComponent<EndlessEnemy>().variant, false); ;
        spawning = false;
        start = false;
        waveNumber = 0;
        nextWaveNumber = 0;
        wavesCompleted = 0;
        countdown = this.waves[waveNumber].countdown;
        EndlessUIManager.instance.ShowNextWaveEnemy(waves[0].GetEnemyPrefab().GetComponent<EndlessEnemy>().variant, true);
        EndlessUIManager.instance.ShowGameNotStartedText();
    }
}
