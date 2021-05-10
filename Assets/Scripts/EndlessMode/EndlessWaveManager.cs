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
        UIManager.instance.ShowNextWaveEnemy(waves[0].GetEnemyPrefab().GetComponent<Enemy>().variant, true);
    }

    public override void DoUpdate()
    {
        if (start)
        {
            if (!spawning)
            {
                if (wavesCompleted >= waves.Count)
                {
                    UIManager.instance.ShowNextWaveCountdown(Math.Round(countdown));
                    if (countdown <= 0f)
                    {
                        waveNumber = nextWaveNumber;
                        nextWaveNumber = UnityEngine.Random.Range(0, this.waves.Count);
                        UIManager.instance.ShowNextWaveEnemy(waves[waveNumber].GetEnemyPrefab().GetComponent<Enemy>().variant, false);
                        StartCoroutine(SpawnWave());
                        spawning = true;
                    }
                    countdown -= Time.deltaTime;
                }
                else
                {
                    if (waveNumber < this.waves.Count)
                    {
                        UIManager.instance.ShowNextWaveCountdown(Math.Round(countdown));
                        if (countdown <= 0f)
                        {
                            UIManager.instance.ShowNextWaveEnemy(waves[waveNumber].GetEnemyPrefab().GetComponent<Enemy>().variant, false);
                            StartCoroutine(SpawnWave());
                            spawning = true;
                            waveNumber++;
                        }
                        countdown -= Time.deltaTime;
                    }
                    else
                    {
                        nextWaveNumber = UnityEngine.Random.Range(0, this.waves.Count);
                    }

                }
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
        Debug.Log("Spawning wave " + (waveNumber + 1) + "!");
        UIManager.instance.ShowSpawnWaveButton(false);
        EndlessWave wave = this.waves[waveNumber];
        if (waveNumber + 1 < waves.Count)
            UIManager.instance.ShowNextWaveEnemy(waves[waveNumber + 1].GetEnemyPrefab().GetComponent<Enemy>().variant, true);
        Transform spawnPoint = Waypoints.instance.GetNextWaypoint(wave.GetAssignedPathIndex(), -1);
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
            UIManager.instance.ShowNextWaveEnemy(waves[waveNumber + 1].GetEnemyPrefab().GetComponent<Enemy>().variant, false);
        else if (waveNumber == waves.Count)
        {
            UIManager.instance.ShowNextWaveEnemy(waves[waveNumber - 1].GetEnemyPrefab().GetComponent<Enemy>().variant, false);
            UIManager.instance.ShowSpawnWaveButton(false);
        }
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
