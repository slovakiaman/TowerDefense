using System;
using UnityEngine;
using Random = UnityEngine.Random;

//namespace EndlessMode.Scripts.Managers
public class EndlessValueCalculator : MonoBehaviour
{
    public static EndlessValueCalculator instance;

    private void Awake()
    {
        instance = this;
    }

    public bool IsEventActive(EndlessLevelEventID eventID)
    {
        return EndlessEventManager.instance.activeEvents.ContainsKey(eventID);
    }

    //toto sa zavolá pri vytváraní nepriateľa v EndlessWaveManager
    public float CalculateEnemyHP(EndlessWave wave)
    {
        if (IsEventActive(EndlessLevelEventID.MONSTER_BUFF_HP))
            return wave.hp * 1.2f;
        if (IsEventActive(EndlessLevelEventID.MONSTER_DEBUFF_HP))
            return wave.hp * .8f;
        return wave.hp;
    }

    public float CalculateEnemySpeed(EndlessWave wave)
    {
        if (IsEventActive(EndlessLevelEventID.MONSTER_BUFF_SPEED))
            return wave.speed * 1.5f;
        if (IsEventActive(EndlessLevelEventID.MONSTER_DEBUFF_SPEED))
            return wave.speed * 0.5f;
        return wave.speed;
    }

    public int CalculateEnemyCount(EndlessWave wave)
    {
        if (IsEventActive(EndlessLevelEventID.MONSTER_BUFF_COUNT))
        {
            //znížiť skóre za navyše
            ScoreManager.instance.AddScore(- wave.moneyReward);
            return wave.enemyCount + 1;
        }

        if (IsEventActive(EndlessLevelEventID.MONSTER_DEBUFF_COUNT) && wave.enemyCount > 1)
        {
            //zvýšiť skóre za chýbajúceho
            ScoreManager.instance.AddScore(wave.moneyReward);
            return wave.enemyCount - 1;
        }
        return wave.enemyCount;
    }
    
    public int CalculateEnemyMoneyReward(EndlessWave wave)
    {
        if (IsEventActive(EndlessLevelEventID.MONSTER_BUFF_MONEY))
        {
            return (int)(wave.moneyReward * 1.2f);
        }

        if (IsEventActive(EndlessLevelEventID.MONSTER_DEBUFF_MONEY))
        {
            return (int)(wave.moneyReward * .8f);
        }
        return wave.moneyReward;
    }

    public bool IsTowerEnabled(EndlessTower tower)
    {
        if (tower.towerType == TowerType.BALISTA && IsEventActive(EndlessLevelEventID.BALISTA_DISABLED))
            return false;
        if (tower.towerType == TowerType.CANNON && IsEventActive(EndlessLevelEventID.CANON_DISABLED))
            return false;
        if (tower.towerType == TowerType.CRYSTAL && IsEventActive(EndlessLevelEventID.CRYSTAL_DISABLED))
            return false;
        if (tower.towerType == TowerType.TESLA && IsEventActive(EndlessLevelEventID.TESLA_DISABLED))
            return false;
        if (tower.towerType == TowerType.SPINNER && IsEventActive(EndlessLevelEventID.SPINNER_DISABLED))
            return false;
        return true;
    }

    public int CalculateTowerDMG(EndlessTower tower)
    {
        float dmgModifier = 1;
        if (IsEventActive(EndlessLevelEventID.TOWER_BUFF_DMG))
            dmgModifier += .2f;

        if (IsEventActive(EndlessLevelEventID.TOWER_DEBUFF_DMG))
            dmgModifier -= .2f;

        if ((tower.towerType == TowerType.BALISTA && IsEventActive(EndlessLevelEventID.BALISTA_BUFF_DMG)) || 
            (tower.towerType == TowerType.CANNON && IsEventActive(EndlessLevelEventID.CANON_BUFF_DMG)) ||
            (tower.towerType == TowerType.CRYSTAL && IsEventActive(EndlessLevelEventID.CRYSTAL_BUFF_DMG)) ||
            (tower.towerType == TowerType.TESLA && IsEventActive(EndlessLevelEventID.TESLA_BUFF_DMG)) ||
            (tower.towerType == TowerType.SPINNER && IsEventActive(EndlessLevelEventID.SPINNER_BUFF_DMG)))
            dmgModifier += .5f;
        
        if ((tower.towerType == TowerType.BALISTA && IsEventActive(EndlessLevelEventID.BALISTA_DEBUFF_DMG)) || 
            (tower.towerType == TowerType.CANNON && IsEventActive(EndlessLevelEventID.CANON_DEBUFF_DMG)) ||
            (tower.towerType == TowerType.CRYSTAL && IsEventActive(EndlessLevelEventID.CRYSTAL_DEBUFF_DMG)) ||
            (tower.towerType == TowerType.TESLA && IsEventActive(EndlessLevelEventID.TESLA_DEBUFF_DMG)) ||
            (tower.towerType == TowerType.SPINNER && IsEventActive(EndlessLevelEventID.SPINNER_DEBUFF_DMG)))
            dmgModifier -= .5f;

        if (IsEventActive(EndlessLevelEventID.TOWER_RANDOM_INSTAKILL))
        {
            if (Random.Range(0, 100) <= 5)
                return 100000;
        }
        
        return (int)(tower.GetTowerDamage() * dmgModifier);
    }

    public float CalculateTowerSPEED(EndlessTower tower)
    {
        float speedModifier = 1;
        if (IsEventActive(EndlessLevelEventID.TOWER_BUFF_SPEED))
            speedModifier += .2f;

        if (IsEventActive(EndlessLevelEventID.TOWER_DEBUFF_SPEED))
            speedModifier -= .2f;

        if ((tower.towerType == TowerType.BALISTA && IsEventActive(EndlessLevelEventID.BALISTA_BUFF_SPEED)) || 
            (tower.towerType == TowerType.CANNON && IsEventActive(EndlessLevelEventID.CANON_BUFF_SPEED)) ||
            (tower.towerType == TowerType.CRYSTAL && IsEventActive(EndlessLevelEventID.CRYSTAL_BUFF_SPEED)) ||
            (tower.towerType == TowerType.TESLA && IsEventActive(EndlessLevelEventID.TESLA_BUFF_SPEED)) ||
            (tower.towerType == TowerType.SPINNER && IsEventActive(EndlessLevelEventID.SPINNER_BUFF_SPEED)))
            speedModifier += .5f;
        
        if ((tower.towerType == TowerType.BALISTA && IsEventActive(EndlessLevelEventID.BALISTA_DEBUFF_SPEED)) || 
            (tower.towerType == TowerType.CANNON && IsEventActive(EndlessLevelEventID.CANON_DEBUFF_SPEED)) ||
            (tower.towerType == TowerType.CRYSTAL && IsEventActive(EndlessLevelEventID.CRYSTAL_DEBUFF_SPEED)) ||
            (tower.towerType == TowerType.TESLA && IsEventActive(EndlessLevelEventID.TESLA_DEBUFF_SPEED)) ||
            (tower.towerType == TowerType.SPINNER && IsEventActive(EndlessLevelEventID.SPINNER_DEBUFF_SPEED)))
            speedModifier -= .5f;

        return tower.GetTowerFireRate() * speedModifier;
    }

    public int CalculateTowerCOST(EndlessTower tower)
    {
        float costModifier = 1;
        if (IsEventActive(EndlessLevelEventID.TOWER_BUFF_COST))
            costModifier += .2f;

        if (IsEventActive(EndlessLevelEventID.TOWER_DEBUFF_COST))
            costModifier -= .2f;

        if ((tower.towerType == TowerType.BALISTA && IsEventActive(EndlessLevelEventID.BALISTA_BUFF_COST)) || 
            (tower.towerType == TowerType.CANNON && IsEventActive(EndlessLevelEventID.CANON_BUFF_COST)) ||
            (tower.towerType == TowerType.CRYSTAL && IsEventActive(EndlessLevelEventID.CRYSTAL_BUFF_COST)) ||
            (tower.towerType == TowerType.TESLA && IsEventActive(EndlessLevelEventID.TESLA_BUFF_COST)) ||
            (tower.towerType == TowerType.SPINNER && IsEventActive(EndlessLevelEventID.SPINNER_BUFF_COST)))
            costModifier += .3f;
        
        if ((tower.towerType == TowerType.BALISTA && IsEventActive(EndlessLevelEventID.BALISTA_DEBUFF_COST)) || 
            (tower.towerType == TowerType.CANNON && IsEventActive(EndlessLevelEventID.CANON_DEBUFF_COST)) ||
            (tower.towerType == TowerType.CRYSTAL && IsEventActive(EndlessLevelEventID.CRYSTAL_DEBUFF_COST)) ||
            (tower.towerType == TowerType.TESLA && IsEventActive(EndlessLevelEventID.TESLA_DEBUFF_COST)) ||
            (tower.towerType == TowerType.SPINNER && IsEventActive(EndlessLevelEventID.SPINNER_DEBUFF_COST)))
            costModifier -= .3f;

        return (int)(tower.GetTowerPrice() * costModifier);
    }
    
    public float CalculateTimerDeltaTime(float deltaTime)
    {
        if (IsEventActive(EndlessLevelEventID.TIMER_SPEED_UP))
            deltaTime = deltaTime * 1.5f;

        if (IsEventActive(EndlessLevelEventID.TIMER_SPEED_DOWN))
            deltaTime = deltaTime * .5f;

        return deltaTime;
    }
    
}