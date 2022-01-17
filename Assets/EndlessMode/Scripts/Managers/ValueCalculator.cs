using EndlessMode.Events;
using EndlessMode.Towers;
using NormalMode.Managers;

namespace EndlessMode.Managers
{
    using EndlessMode.Waves;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class ValueCalculator : MonoBehaviour
    {
        public static ValueCalculator instance;

        private void Awake()
        {
            instance = this;
        }

        public bool IsEventActive(EventID eventID)
        {
            return EventManager.instance.activeEvents.ContainsKey(eventID);
        }

        //toto sa zavolá pri vytváraní nepriateľa v WaveManager
        public float CalculateEnemyHP(Wave wave)
        {
            if (IsEventActive(EventID.MONSTER_BUFF_HP))
                return wave.hp * 1.2f;
            if (IsEventActive(EventID.MONSTER_DEBUFF_HP))
                return wave.hp * .8f;
            return wave.hp;
        }

        public float CalculateEnemySpeed(Wave wave)
        {
            if (IsEventActive(EventID.MONSTER_BUFF_SPEED))
                return wave.speed * 1.5f;
            if (IsEventActive(EventID.MONSTER_DEBUFF_SPEED))
                return wave.speed * 0.5f;
            return wave.speed;
        }

        public int CalculateEnemyCount(Wave wave)
        {
            if (IsEventActive(EventID.MONSTER_BUFF_COUNT))
            {
                //znížiť skóre za navyše
                ScoreManager.instance.AddScore(- wave.moneyReward);
                return wave.enemyCount + 1;
            }

            if (IsEventActive(EventID.MONSTER_DEBUFF_COUNT) && wave.enemyCount > 1)
            {
                //zvýšiť skóre za chýbajúceho
                ScoreManager.instance.AddScore(wave.moneyReward);
                return wave.enemyCount - 1;
            }
            return wave.enemyCount;
        }
        
        public int CalculateEnemyMoneyReward(Wave wave)
        {
            if (IsEventActive(EventID.MONSTER_BUFF_MONEY))
            {
                return (int)(wave.moneyReward * 1.2f);
            }

            if (IsEventActive(EventID.MONSTER_DEBUFF_MONEY))
            {
                return (int)(wave.moneyReward * .8f);
            }
            return wave.moneyReward;
        }

        public bool IsTowerEnabled(Tower tower)
        {
            if (tower.towerType == TowerType.BALISTA && IsEventActive(EventID.BALISTA_DISABLED))
                return false;
            if (tower.towerType == TowerType.CANNON && IsEventActive(EventID.CANON_DISABLED))
                return false;
            if (tower.towerType == TowerType.CRYSTAL && IsEventActive(EventID.CRYSTAL_DISABLED))
                return false;
            if (tower.towerType == TowerType.TESLA && IsEventActive(EventID.TESLA_DISABLED))
                return false;
            if (tower.towerType == TowerType.SPINNER && IsEventActive(EventID.SPINNER_DISABLED))
                return false;
            return true;
        }

        public int CalculateTowerDMG(Tower tower)
        {
            float dmgModifier = 1;
            if (IsEventActive(EventID.TOWER_BUFF_DMG))
                dmgModifier += .2f;

            if (IsEventActive(EventID.TOWER_DEBUFF_DMG))
                dmgModifier -= .2f;

            if ((tower.towerType == TowerType.BALISTA && IsEventActive(EventID.BALISTA_BUFF_DMG)) || 
                (tower.towerType == TowerType.CANNON && IsEventActive(EventID.CANON_BUFF_DMG)) ||
                (tower.towerType == TowerType.CRYSTAL && IsEventActive(EventID.CRYSTAL_BUFF_DMG)) ||
                (tower.towerType == TowerType.TESLA && IsEventActive(EventID.TESLA_BUFF_DMG)) ||
                (tower.towerType == TowerType.SPINNER && IsEventActive(EventID.SPINNER_BUFF_DMG)))
                dmgModifier += .5f;
            
            if ((tower.towerType == TowerType.BALISTA && IsEventActive(EventID.BALISTA_DEBUFF_DMG)) || 
                (tower.towerType == TowerType.CANNON && IsEventActive(EventID.CANON_DEBUFF_DMG)) ||
                (tower.towerType == TowerType.CRYSTAL && IsEventActive(EventID.CRYSTAL_DEBUFF_DMG)) ||
                (tower.towerType == TowerType.TESLA && IsEventActive(EventID.TESLA_DEBUFF_DMG)) ||
                (tower.towerType == TowerType.SPINNER && IsEventActive(EventID.SPINNER_DEBUFF_DMG)))
                dmgModifier -= .5f;

            if (IsEventActive(EventID.TOWER_RANDOM_INSTAKILL))
            {
                if (Random.Range(0, 100) <= 5)
                    return 100000;
            }
            
            return (int)(tower.GetTowerDamage() * dmgModifier);
        }

        public float CalculateTowerSPEED(Tower tower)
        {
            float speedModifier = 1;
            if (IsEventActive(EventID.TOWER_BUFF_SPEED))
                speedModifier += .2f;

            if (IsEventActive(EventID.TOWER_DEBUFF_SPEED))
                speedModifier -= .2f;

            if ((tower.towerType == TowerType.BALISTA && IsEventActive(EventID.BALISTA_BUFF_SPEED)) || 
                (tower.towerType == TowerType.CANNON && IsEventActive(EventID.CANON_BUFF_SPEED)) ||
                (tower.towerType == TowerType.CRYSTAL && IsEventActive(EventID.CRYSTAL_BUFF_SPEED)) ||
                (tower.towerType == TowerType.TESLA && IsEventActive(EventID.TESLA_BUFF_SPEED)) ||
                (tower.towerType == TowerType.SPINNER && IsEventActive(EventID.SPINNER_BUFF_SPEED)))
                speedModifier += .5f;
            
            if ((tower.towerType == TowerType.BALISTA && IsEventActive(EventID.BALISTA_DEBUFF_SPEED)) || 
                (tower.towerType == TowerType.CANNON && IsEventActive(EventID.CANON_DEBUFF_SPEED)) ||
                (tower.towerType == TowerType.CRYSTAL && IsEventActive(EventID.CRYSTAL_DEBUFF_SPEED)) ||
                (tower.towerType == TowerType.TESLA && IsEventActive(EventID.TESLA_DEBUFF_SPEED)) ||
                (tower.towerType == TowerType.SPINNER && IsEventActive(EventID.SPINNER_DEBUFF_SPEED)))
                speedModifier -= .5f;

            return tower.GetTowerFireRate() * speedModifier;
        }

        public int CalculateTowerCOST(Tower tower)
        {
            float costModifier = 1;
            if (IsEventActive(EventID.TOWER_BUFF_COST))
                costModifier += .2f;

            if (IsEventActive(EventID.TOWER_DEBUFF_COST))
                costModifier -= .2f;

            if ((tower.towerType == TowerType.BALISTA && IsEventActive(EventID.BALISTA_BUFF_COST)) || 
                (tower.towerType == TowerType.CANNON && IsEventActive(EventID.CANON_BUFF_COST)) ||
                (tower.towerType == TowerType.CRYSTAL && IsEventActive(EventID.CRYSTAL_BUFF_COST)) ||
                (tower.towerType == TowerType.TESLA && IsEventActive(EventID.TESLA_BUFF_COST)) ||
                (tower.towerType == TowerType.SPINNER && IsEventActive(EventID.SPINNER_BUFF_COST)))
                costModifier += .3f;
            
            if ((tower.towerType == TowerType.BALISTA && IsEventActive(EventID.BALISTA_DEBUFF_COST)) || 
                (tower.towerType == TowerType.CANNON && IsEventActive(EventID.CANON_DEBUFF_COST)) ||
                (tower.towerType == TowerType.CRYSTAL && IsEventActive(EventID.CRYSTAL_DEBUFF_COST)) ||
                (tower.towerType == TowerType.TESLA && IsEventActive(EventID.TESLA_DEBUFF_COST)) ||
                (tower.towerType == TowerType.SPINNER && IsEventActive(EventID.SPINNER_DEBUFF_COST)))
                costModifier -= .3f;

            return (int)(tower.GetTowerPrice() * costModifier);
        }
        
        public float CalculateTimerDeltaTime(float deltaTime)
        {
            if (IsEventActive(EventID.TIMER_SPEED_UP))
                deltaTime = deltaTime * 1.5f;

            if (IsEventActive(EventID.TIMER_SPEED_DOWN))
                deltaTime = deltaTime * .5f;

            return deltaTime;
        }
        
    }    
}