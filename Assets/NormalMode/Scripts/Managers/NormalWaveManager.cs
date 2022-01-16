using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class NormalWaveManager : WaveManager
{
    public List<Wave> waves;

    public override void Init()
    {
        countdown = this.waves[waveNumber].countdown;
        DialogueManager.instance.ShowDialogue("start");
        UIManager.instance.ShowNextWaveEnemy(waves[0].GetEnemyPrefab().GetComponent<Enemy>().variant, true);
        waveNumber = 0;
    }

    public override void DoUpdate()
    {
        if (start)
        {
            if (!spawning)
            {
                if (waveNumber < this.waves.Count)
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
                    if (checkVictoryTimer <= 0)
                    {
                        CheckVictory();
                        checkVictoryTimer = 1f;
                    }
                    checkVictoryTimer -= Time.deltaTime;
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

    private void CheckVictory()
    {
        GameObject enemyObject = GameObject.FindGameObjectWithTag("Enemy");
        if (enemyObject == null)
            PlayerManager.instance.SetVictory(true);
    }


    public override IEnumerator SpawnWave()
    {
        
        spawning = true;
        DialogueManager.instance.ShowDialogue(waveNumber.ToString());
        Debug.Log("Spawning wave " + (waveNumber + 1) + "!");
        UIManager.instance.ShowSpawnWaveButton(false);
        Wave wave = this.waves[waveNumber];
        Transform spawnPoint = ((NormalWaypoints)Waypoints.instance).GetNextWaypoint(wave.pathNumber, -1);
        for (int i = 0; i < wave.enemyCount; i++)
        {
            GameObject enemyObject = Instantiate(wave.GetEnemyPrefab(), spawnPoint.position, wave.GetEnemyPrefab().transform.rotation);
            enemyObject.GetComponent<Enemy>().SetupValues(wave.pathNumber, wave.speed, wave.hp, wave.moneyReward, wave.scale);
            yield return new WaitForSeconds(1.7f - (wave.speed * 0.15f));
        }
        UIManager.instance.ShowNextWaveEnemy(waves[waveNumber].GetEnemyPrefab().GetComponent<Enemy>().variant, false);
        waveNumber++;
        if (waveNumber < waves.Count)
        {
            UIManager.instance.ShowNextWaveEnemy(waves[waveNumber].GetEnemyPrefab().GetComponent<Enemy>().variant, true);
            countdown = this.waves[waveNumber].countdown;
        }
        spawning = false;
        UIManager.instance.ShowSpawnWaveButton(true);
    }

    public override void StartSpawner()
    {
        this.start = true;
        DialogueManager.instance.Reset();
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
        countdown = this.waves[waveNumber].countdown;
        UIManager.instance.ShowNextWaveEnemy(waves[0].GetEnemyPrefab().GetComponent<Enemy>().variant, true);
        UIManager.instance.ShowGameNotStartedText();
    }
}
