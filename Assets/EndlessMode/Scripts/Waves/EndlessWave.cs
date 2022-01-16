using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Waves/EndlessWave")]
public class EndlessWave : ScriptableObject
{
    [SerializeField] private EnemyVariant enemy;
    public Difficulty waveDifficulty;
    public int enemyCount;

    public float speed;
    public float hp;
    public float scale;
    public float countdown;
    public int moneyReward;
    private GameObject enemyPrefab = null;
    private int assignedPathIndex;

    public GameObject GetEnemyPrefab()
    {
        AssignPrefab();
        return this.enemyPrefab;
    }

    private void AssignPrefab()
    {
        switch (enemy)
        {
            case EnemyVariant.SLIME_RED:
                enemyPrefab = ConstantsManager.instance.enemySlimeRedPrefab;
                break;
            case EnemyVariant.SLIME_GREEN:
                enemyPrefab = ConstantsManager.instance.enemySlimeGreenPrefab;
                break;
            case EnemyVariant.SLIME_YELLOW:
                enemyPrefab = ConstantsManager.instance.enemySlimeYellowPrefab;
                break;
            case EnemyVariant.TURTLE_BLUE:
                enemyPrefab = ConstantsManager.instance.enemyTurtleBluePrefab;
                break;
            case EnemyVariant.TURTLE_BLACK:
                enemyPrefab = ConstantsManager.instance.enemyTurtleBlackPrefab;
                break;
            case EnemyVariant.TURTLE_WHITE:
                enemyPrefab = ConstantsManager.instance.enemyTurtleWhitePrefab;
                break;
            case EnemyVariant.MUMMY_GREY:
                enemyPrefab = ConstantsManager.instance.enemyMummyGreyPrefab;
                break;
            case EnemyVariant.MUMMY_WHITE:
                enemyPrefab = ConstantsManager.instance.enemyMummyWhitePrefab;
                break;
            case EnemyVariant.MUMMY_RED:
                enemyPrefab = ConstantsManager.instance.enemyMummyRedPrefab;
                break;
            case EnemyVariant.LICH_PURPLE:
                enemyPrefab = ConstantsManager.instance.enemyLichPurplePrefab;
                break;
            case EnemyVariant.LICH_GREEN:
                enemyPrefab = ConstantsManager.instance.enemyLichGreenPrefab;
                break;
            case EnemyVariant.LICH_RED:
                enemyPrefab = ConstantsManager.instance.enemyLichRedPrefab;
                break;
            case EnemyVariant.GOLEM_GREEN:
                enemyPrefab = ConstantsManager.instance.enemyGolemGreenPrefab;
                break;
            case EnemyVariant.GOLEM_RED:
                enemyPrefab = ConstantsManager.instance.enemyGolemRedPrefab;
                break;
            case EnemyVariant.GOLEM_RED2:
                enemyPrefab = ConstantsManager.instance.enemyGolemRed2Prefab;
                break;
            case EnemyVariant.BEHOLDER_PURPLE:
                enemyPrefab = ConstantsManager.instance.enemyBeholderPurplePrefab;
                break;
            case EnemyVariant.BEHOLDER_CYAN:
                enemyPrefab = ConstantsManager.instance.enemyBeholderCyanPrefab;
                break;
            case EnemyVariant.BEHOLDER_GREEN:
                enemyPrefab = ConstantsManager.instance.enemyBeholderGreenPrefab;
                break;
            case EnemyVariant.BOSS1:
                enemyPrefab = ConstantsManager.instance.enemyBoss1Prefab;
                break;
            case EnemyVariant.BOSS2:
                enemyPrefab = ConstantsManager.instance.enemyBoss2Prefab;
                break;
            case EnemyVariant.BOSS3:
                enemyPrefab = ConstantsManager.instance.enemyBoss3Prefab;
                break;
            case EnemyVariant.BOSS4:
                enemyPrefab = ConstantsManager.instance.enemyBoss4Prefab;
                break;
        }
    }

    public int GetAssignedPathIndex()
    {
        
        return this.assignedPathIndex;
    }

    public void SetAssignedPathIndex(int index)
    {
        this.assignedPathIndex = index;
    }
}
