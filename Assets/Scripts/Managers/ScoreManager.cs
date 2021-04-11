using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private Scores _scores;
    private ulong _score;

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
        try
        {
            string jsonString = File.ReadAllText(Application.persistentDataPath + "/Score.json");
            _scores = JsonUtility.FromJson<Scores>(jsonString);
        }
        catch(FileNotFoundException e)
        {
            Debug.Log("No saved score");
        }
        catch (IOException error)
        {
            Debug.Log(error.Message);
        }
        

        
    }

    public void setScore(ulong score)
    {
        _score = score;
        Debug.Log("Change score" + _score);
    }

    public void AddScore(int score)
    {
        _score += (ulong) score;
        Debug.Log("Added score" + _score);
    }

    public ulong GetScore()
    {
        return _score;
    }

    public bool getScoreByLevel(in string levelName,out ulong score)
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
    public ulong levelScore;

    public Score(string name, ulong score)
    {
        levelName = name;
        levelScore = score;
    }
}