using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantsManager: MonoBehaviour
{
    public static ConstantsManager instance;

    public void Awake()
    {
        instance = this;
        if (balistaTowerPrefab1 == null)
            Debug.LogError("No balista tower 1 found");
        if (cannonTowerPrefab1 == null)
            Debug.LogError("No cannon tower 1 found");
        if (crystalTowerPrefab1 == null)
            Debug.LogError("No crystal tower 1 found");
        if (teslaTowerPrefab1 == null)
            Debug.LogError("No tesla tower 1 found");
        if (spinnerTowerPrefab1 == null)
            Debug.LogError("No spinner tower 1 found");

        if (enemySlimeRedPrefab == null)
            Debug.LogError("No slime enemy found");
        if (enemySlimeGreenPrefab == null)
            Debug.LogError("No slime enemy found");
        if (enemySlimeYellowPrefab == null)
            Debug.LogError("No slime enemy found");
        if (enemyTurtleBluePrefab == null)
            Debug.LogError("No turtle enemy found");
        if (enemyTurtleBlackPrefab == null)
            Debug.LogError("No turtle enemy found");
        if (enemyTurtleWhitePrefab == null)
            Debug.LogError("No turtle enemy found");
        if (enemyMummyGreyPrefab == null)
            Debug.LogError("No mummy enemy found");
        if (enemyMummyWhitePrefab == null)
            Debug.LogError("No mummy enemy found");
        if (enemyMummyRedPrefab == null)
            Debug.LogError("No mummy enemy found");
        if (enemyLichPurplePrefab == null)
            Debug.LogError("No lich enemy found");
        if (enemyLichGreenPrefab == null)
            Debug.LogError("No lich enemy found");
        if (enemyLichRedPrefab == null)
            Debug.LogError("No lich enemy found");
        if (enemyGolemGreenPrefab == null)
            Debug.LogError("No golem enemy found");
        if (enemyGolemRedPrefab == null)
            Debug.LogError("No golem enemy found");
        if (enemyGolemRed2Prefab == null)
            Debug.LogError("No golem enemy found");
        if (enemyBeholderPurplePrefab == null)
            Debug.LogError("No beholder enemy found");
        if (enemyBeholderCyanPrefab == null)
            Debug.LogError("No beholder enemy found");
        if (enemyBeholderGreenPrefab == null)
            Debug.LogError("No beholder enemy found");
    }

    [Header("Tower prefabs")]
    public GameObject balistaTowerPrefab1;
    public GameObject cannonTowerPrefab1;
    public GameObject crystalTowerPrefab1;
    public GameObject teslaTowerPrefab1;
    public GameObject spinnerTowerPrefab1;

    [Header("Enemy prefabs")]
    public GameObject enemySlimeRedPrefab;
    public GameObject enemySlimeGreenPrefab;
    public GameObject enemySlimeYellowPrefab;
    public GameObject enemyTurtleBluePrefab;
    public GameObject enemyTurtleBlackPrefab;
    public GameObject enemyTurtleWhitePrefab;
    public GameObject enemyMummyGreyPrefab;
    public GameObject enemyMummyWhitePrefab;
    public GameObject enemyMummyRedPrefab;
    public GameObject enemyLichPurplePrefab;
    public GameObject enemyLichGreenPrefab;
    public GameObject enemyLichRedPrefab;
    public GameObject enemyGolemGreenPrefab;
    public GameObject enemyGolemRedPrefab;
    public GameObject enemyGolemRed2Prefab;
    public GameObject enemyBeholderPurplePrefab;
    public GameObject enemyBeholderCyanPrefab;
    public GameObject enemyBeholderGreenPrefab;


}
