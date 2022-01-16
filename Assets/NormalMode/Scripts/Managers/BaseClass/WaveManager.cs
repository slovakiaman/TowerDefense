using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public abstract class WaveManager : MonoBehaviour
{
    public float nextWaveTime = 5.5f;
    public float countdown;
    public int waveNumber = 0;
    protected bool spawning = false;
    protected bool start = false;
    protected float checkVictoryTimer = 0f;

    void Start()
    {
        Init();
    }

    public abstract void Init();

    void Update()
    {
        DoUpdate();
    }

    public abstract void DoUpdate();

    public abstract IEnumerator SpawnWave();

    public abstract void StartSpawner();

    public void spawnAnotherWave()
    {
        countdown = 0;
        UIManager.instance.ShowSpawnWaveButton(false);
        PlayerManager.instance.AddMoney(200);
    }

    public abstract void ResetSpawner();
}
