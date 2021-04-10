using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private Scores _scores;
    private int _score;

    private void Start()
    {
        instance = this;
        instance._scores = new Scores();
        instance._score = 0;
        instance.LoadFromJson();
    }

    public void SaveIntoJson(string levelName)
    {
        Score score = null;
        if (_scores.GetLevelByName(levelName,out score))
        {
            if (score.levelScore > _score)
            {
                return;
            }
            else
            {
                score.levelScore = _score;
            }
        }
        else
        {
            _scores.scores.Add(new Score(levelName, _score));
        }
        
        _score = 0;
        string data = JsonUtility.ToJson(_scores);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/Score.json", data);
    }

    public void LoadFromJson()
    {
        _scores = JsonUtility.FromJson<Scores>(System.IO.File.ReadAllText(Application.persistentDataPath + "/Score.json"));
    }

    public void setScore(int score)
    {
        _score = score;
    }

    public void AddScore(int score)
    {
        _score += score;
    }

    public bool getScoreByLevel(in string levelName,out int score)
    {
        Score score1 = null;
        if (_scores.GetLevelByName(in levelName, out score1))
        {
            score = score1.levelScore;
            return true;
        }
        score = 0;
        return false;
    }
}

[System.Serializable]
public class Scores
{
    public List<Score> scores = new List<Score>();

    public bool GetLevelByName(in string levelName,out Score _score)
    {
        foreach (Score score in scores)
        {
            if (score.levelName.Equals(levelName))
            {
                _score = score;
                return true;
            }
        }
        _score = null;
        return false;
    }
}


[System.Serializable]
public class Score
{
    public string levelName;
    public int levelScore;

    public Score(string name,int score)
    {
        levelName = name;
        levelScore = score;
    }
}