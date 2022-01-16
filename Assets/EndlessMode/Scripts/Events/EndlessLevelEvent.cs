using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class EndlessLevelEvent
{
    public EndlessLevelEventID id;
    public bool positiveEffect;
    public string name;
    public string description;
    [HideInInspector] public int startingWave;
    public int duration;
    public int conflictingGroup;
}
public enum EndlessLevelEventID
{
    // ----------------GROUP EFFECTS--------------------
    //ENEMIES
    MONSTER_BUFF_HP,
    MONSTER_BUFF_SPEED,
    MONSTER_BUFF_COUNT,
    MONSTER_BUFF_SPEED_RANDOM,
    MONSTER_BUFF_MONEY,
    MONSTER_ONHIT_SPEED,
    MONSTER_DEBUFF_HP,
    MONSTER_DEBUFF_COUNT,
    MONSTER_DEBUFF_SPEED,
    MONSTER_DEBUFF_SPEED_RANDOM,
    MONSTER_DEBUFF_MONEY,
    MONSTER_ONHIT_SLOW,
    MONSTER_RANDOM_INVINCIBILITY,
    
    //TIMER
    TIMER_SPEED_UP,
    TIMER_SPEED_DOWN,
    
    //TOWER
    TOWER_BUFF_DMG,
    TOWER_BUFF_SPEED,
    TOWER_BUFF_COST,
    TOWER_DEBUFF_DMG,
    TOWER_DEBUFF_SPEED,
    TOWER_DEBUFF_COST,
    TOWER_RANDOM_INSTAKILL,
    

    //BALISTA SPECIFIC
    BALISTA_DISABLED,
    BALISTA_BUFF_DMG,
    BALISTA_BUFF_SPEED,
    BALISTA_BUFF_COST,
    BALISTA_DEBUFF_DMG,
    BALISTA_DEBUFF_SPEED,
    BALISTA_DEBUFF_COST,
    
    //CANNON SPECIFIC
    CANON_DISABLED,
    CANON_BUFF_DMG,
    CANON_BUFF_SPEED,
    CANON_BUFF_COST,
    CANON_DEBUFF_DMG,
    CANON_DEBUFF_SPEED,
    CANON_DEBUFF_COST,
    
    //CRYSTAL SPECIFIC BUFFS
    CRYSTAL_DISABLED,
    CRYSTAL_BUFF_DMG,
    CRYSTAL_BUFF_SPEED,
    CRYSTAL_BUFF_COST,
    CRYSTAL_DEBUFF_DMG,
    CRYSTAL_DEBUFF_SPEED,
    CRYSTAL_DEBUFF_COST,
    
    //TESLA SPECIFIC BUFFS
    TESLA_DISABLED,
    TESLA_BUFF_DMG,
    TESLA_BUFF_SPEED,
    TESLA_BUFF_COST,
    TESLA_DEBUFF_DMG,
    TESLA_DEBUFF_SPEED,
    TESLA_DEBUFF_COST,
    
    //SPINNER SPECIFIC BUFFS
    SPINNER_DISABLED,
    SPINNER_BUFF_DMG,
    SPINNER_BUFF_SPEED,
    SPINNER_BUFF_COST,
    SPINNER_DEBUFF_DMG,
    SPINNER_DEBUFF_SPEED,
    SPINNER_DEBUFF_COST,
    
    
    /*
     * Overdriwe - rýchlejšie tiká timer + mobky + rýchlejšie strieľajú towerky?
        Slowdown - pomalšie tiká čas + mobky + rýchlejšie strieľajú towerky?

        Reinforcements - Príde o mobku viac
        Deserters - Príde o mobku menej

        towerka # stojí menej
        towerka # stojí viac

        mobky majú viac hp
        mobky majú menej hp

        objaví sa barikáda na určenom mieste

        towerka # dáva viac dmg
        towerka # dáva menej dmg
        
        mobka náhodne zrýchľuje
        mobka náhodne zastaví
     */
}



