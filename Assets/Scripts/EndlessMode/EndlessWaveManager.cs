using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class EndlessWaveManager : WaveManager
{
    public List<EndlessWave> waves;
    public int nextWaveNumber;
    public int wavesCompleted;
    public float endlessCoeficient = 1.3f;

    public override void Init()
    {
        countdown = this.waves[waveNumber].countdown;
        nextWaveNumber = 0;
        UIManager.instance.ShowNextWaveEnemy(waves[0].GetEnemyPrefab().GetComponent<Enemy>().variant, true);
    }

    public override void DoUpdate()
    {
        if (start)
        {
            if (!spawning)
            {
                UIManager.instance.ShowNextWaveCountdown(Math.Round(countdown));
                if (countdown <= 0f)
                {
                    StartCoroutine(SpawnWave());
                }
                countdown -= Time.deltaTime;
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

    public override IEnumerator SpawnWave()
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

        EndlessWave wave = this.waves[waveNumber];
        int assignedPath = ((EndlessWaypoints)Waypoints.instance).AssignPath(wave.waveDifficulty);
        wave.SetAssignedPathIndex(assignedPath);
        Transform spawnPoint = ((EndlessWaypoints)Waypoints.instance).GetNextWaypoint(wave.GetAssignedPathIndex(), -1);
        for (int i = 0; i < wave.enemyCount; i++)
        {
            GameObject enemyObject = Instantiate(wave.GetEnemyPrefab(), spawnPoint.position, wave.GetEnemyPrefab().transform.rotation);
            float enemyHp = wave.hp;
            if (wavesCompleted >= waves.Count)
            {
                float multiplier = (int)(wavesCompleted / waves.Count);
                float multiply = Mathf.Pow(endlessCoeficient, multiplier);
                enemyHp = enemyHp * multiply;
            }
            enemyObject.GetComponent<Enemy>().SetupValues(wave.GetAssignedPathIndex(), wave.speed, enemyHp, wave.moneyReward, wave.scale);
            yield return new WaitForSeconds(1.7f - (wave.speed * 0.15f));
        }
        if (waveNumber < waves.Count)
            countdown = this.waves[waveNumber].countdown;
        spawning = false;
        wavesCompleted++;
        UIManager.instance.ShowNextWaveEnemy(waves[waveNumber].GetEnemyPrefab().GetComponent<Enemy>().variant, false);
        UIManager.instance.ShowNextWaveEnemy(waves[nextWaveNumber].GetEnemyPrefab().GetComponent<Enemy>().variant, true);
        UIManager.instance.ShowSpawnWaveButton(true);
    }

    public override void StartSpawner()
    {
        this.start = true;
    }

public override void ResetSpawner()
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
