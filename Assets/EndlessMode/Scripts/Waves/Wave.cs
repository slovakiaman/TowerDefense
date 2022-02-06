namespace EndlessMode.Waves
{
    using EndlessMode.Managers;
    using UnityEngine;
    [CreateAssetMenu(menuName = "Waves/EndlessWave")]
    public class Wave : ScriptableObject
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
                case EnemyVariant.BAT_PURPLE:
                    enemyPrefab = ConstantsManager.instance.enemyBatPurplePrefab;
                    break;
                case EnemyVariant.BAT_DESERT:
                    enemyPrefab = ConstantsManager.instance.enemyBatDesertPrefab;
                    break;
                case EnemyVariant.BAT_WINTER:
                    enemyPrefab = ConstantsManager.instance.enemyBatWinterPrefab;
                    break;
                case EnemyVariant.SMALLDRAGON_RED:
                    enemyPrefab = ConstantsManager.instance.enemySmallDragonRedPrefab;
                    break;
                case EnemyVariant.SMALLDRAGON_DESERT:
                    enemyPrefab = ConstantsManager.instance.enemySmallDragonDesertPrefab;
                    break;
                case EnemyVariant.SMALLDRAGON_WINTER:
                    enemyPrefab = ConstantsManager.instance.enemySmallDragonWinterPrefab;
                    break;
                case EnemyVariant.MONSTERPLANT_GREEN:
                    enemyPrefab = ConstantsManager.instance.enemyMonsterPlantGreenPrefab;
                    break;
                case EnemyVariant.MONSTERPLANT_DESERT:
                    enemyPrefab = ConstantsManager.instance.enemyMonsterPlantDesertPrefab;
                    break;
                case EnemyVariant.MONSTERPLANT_WINTER:
                    enemyPrefab = ConstantsManager.instance.enemyMonsterPlantWinterPrefab;
                    break;
                case EnemyVariant.ORC_GREEN:
                    enemyPrefab = ConstantsManager.instance.enemyOrcGreenPrefab;
                    break;
                case EnemyVariant.ORC_DESERT:
                    enemyPrefab = ConstantsManager.instance.enemyOrcDesertPrefab;
                    break;
                case EnemyVariant.ORC_WINTER:
                    enemyPrefab = ConstantsManager.instance.enemyOrcWinterPrefab;
                    break;
                case EnemyVariant.SKELETON_PURPLE:
                    enemyPrefab = ConstantsManager.instance.enemySkeletonPurplePrefab;
                    break;
                case EnemyVariant.SKELETON_DESERT:
                    enemyPrefab = ConstantsManager.instance.enemySkeletonDesertPrefab;
                    break;
                case EnemyVariant.SKELETON_WINTER:
                    enemyPrefab = ConstantsManager.instance.enemySkeletonWinterPrefab;
                    break;
                case EnemyVariant.SPIDER_GREEN:
                    enemyPrefab = ConstantsManager.instance.enemySpiderGreenPrefab;
                    break;
                case EnemyVariant.SPIDER_DESERT:
                    enemyPrefab = ConstantsManager.instance.enemySpiderDesertPrefab;
                    break;
                case EnemyVariant.SPIDER_WINTER:
                    enemyPrefab = ConstantsManager.instance.enemySpiderWinterPrefab;
                    break;
                case EnemyVariant.EVILMAGE_PURPLE:
                    enemyPrefab = ConstantsManager.instance.enemyEvilMagePurplePrefab;
                    break;
                case EnemyVariant.EVILMAGE_DESERT:
                    enemyPrefab = ConstantsManager.instance.enemyEvilMageDesertPrefab;
                    break;
                case EnemyVariant.EVILMAGE_WINTER:
                    enemyPrefab = ConstantsManager.instance.enemyEvilMageWinterPrefab;
                    break;
                case EnemyVariant.ROBOGOLEM_GRAY:
                    enemyPrefab = ConstantsManager.instance.enemyRoboGolemGrayPrefab;
                    break;
                case EnemyVariant.ROBOGOLEM_DESERT:
                    enemyPrefab = ConstantsManager.instance.enemyRoboGolemDesertPrefab;
                    break;
                case EnemyVariant.ROBOGOLEM_WINTER:
                    enemyPrefab = ConstantsManager.instance.enemyRoboGolemWinterPrefab;
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
}

