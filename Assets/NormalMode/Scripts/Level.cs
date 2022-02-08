using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public string levelName;
    public ulong levelScore;
    public int index;
    public bool isLocked;

    public Level(string name, ulong score,int _index,bool _isLocked)
    {
        levelName = name;
        levelScore = score;
        index = _index;
        isLocked = _isLocked;
    }
}
