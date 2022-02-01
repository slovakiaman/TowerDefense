namespace NormalMode.Managers
{
    using UnityEngine;
    public class ConstantsManager: MonoBehaviour
    {
        public static ConstantsManager instance;

        public void Awake()
        {
            instance = this;

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
            if (enemyBoss1Prefab == null)
                Debug.LogError("No boss1 enemy found");
            if (enemyBoss2Prefab == null)
                Debug.LogError("No boss2 enemy found");
            if (enemyBoss3Prefab == null)
                Debug.LogError("No boss3 enemy found");
            if (enemyBoss4Prefab == null)
                Debug.LogError("No boss enemy found");
            if (enemyBatPurplePrefab == null)
                Debug.LogError("No bat enemy found");
            if (enemyBatDesertPrefab == null)
                Debug.LogError("No bat desert enemy found");
            if (enemyBatWinterPrefab == null)
                Debug.LogError("No bat winter enemy found");
            if (enemySmallDragonRedPrefab == null)
                Debug.LogError("No small dragon enemy found");
            if (enemySmallDragonDesertPrefab == null)
                Debug.LogError("No small dragon enemy found");
            if (enemySmallDragonWinterPrefab == null)
                Debug.LogError("No small dragon enemy found");
            if (enemyMonsterPlantGreenPrefab == null)
                Debug.LogError("No monster plant green enemy found");
            if (enemyMonsterPlantDesertPrefab == null)
                Debug.LogError("No monster plant desert enemy found");
            if (enemyMonsterPlantWinterPrefab == null)
                Debug.LogError("No monster plant winter enemy found");
            if (enemyOrcGreenPrefab == null)
                Debug.LogError("No orc green enemy found");
            if (enemyOrcDesertPrefab == null)
                Debug.LogError("No orc desert enemy found");
            if (enemyOrcWinterPrefab == null)
                Debug.LogError("No orc winter enemy found");
            if (enemySkeletonPurplePrefab == null)
                Debug.LogError("No skeleton purple enemy found");
            if (enemySkeletonDesertPrefab == null)
                Debug.LogError("No skeleton desert enemy found");
            if (enemySkeletonWinterPrefab == null)
                Debug.LogError("No skeleton winter enemy found");
            if (enemySpiderGreenPrefab == null)
                Debug.LogError("No spider green enemy found");
            if (enemySpiderDesertPrefab == null)
                Debug.LogError("No spider desert enemy found");
            if (enemySpiderWinterPrefab == null)
                Debug.LogError("No spider winter enemy found");
            if (enemyEvilMagePurplePrefab == null)
                Debug.LogError("No evilmage purple enemy found");
            if (enemyEvilMageWinterPrefab == null)
                Debug.LogError("No evilmage winter enemy found");
            if (enemyEvilMageDesertPrefab == null)
                Debug.LogError("No evilmage desert enemy found");
            if (enemyRoboGolemGrayPrefab == null)
                Debug.LogError("No robogolem gray enemy found");
            if (enemyRoboGolemDesertPrefab == null)
                Debug.LogError("No robogolem desert enemy found");
            if (enemyRoboGolemWinterPrefab == null)
                Debug.LogError("No robogolem winter enemy found");

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
        public GameObject enemyBatPurplePrefab;
        public GameObject enemyBatDesertPrefab;
        public GameObject enemyBatWinterPrefab;
        public GameObject enemySmallDragonRedPrefab;
        public GameObject enemySmallDragonDesertPrefab;
        public GameObject enemySmallDragonWinterPrefab;
        public GameObject enemyMonsterPlantGreenPrefab;
        public GameObject enemyMonsterPlantDesertPrefab;
        public GameObject enemyMonsterPlantWinterPrefab;
        public GameObject enemyOrcGreenPrefab;
        public GameObject enemyOrcDesertPrefab;
        public GameObject enemyOrcWinterPrefab;
        public GameObject enemySkeletonPurplePrefab;
        public GameObject enemySkeletonDesertPrefab;
        public GameObject enemySkeletonWinterPrefab;
        public GameObject enemySpiderGreenPrefab;
        public GameObject enemySpiderDesertPrefab;
        public GameObject enemySpiderWinterPrefab;
        public GameObject enemyEvilMagePurplePrefab;
        public GameObject enemyEvilMageDesertPrefab;
        public GameObject enemyEvilMageWinterPrefab;
        public GameObject enemyRoboGolemGrayPrefab;
        public GameObject enemyRoboGolemDesertPrefab;
        public GameObject enemyRoboGolemWinterPrefab;

        [Header("Enemy BOSS prefabs")]
        public GameObject enemyBoss1Prefab;
        public GameObject enemyBoss2Prefab;
        public GameObject enemyBoss3Prefab;
        public GameObject enemyBoss4Prefab;

    }    
}