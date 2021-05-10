using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessWaypoints : MonoBehaviour
{
    public static EndlessWaypoints instance;

    [SerializeField]
    public List<EndlessPath> paths;
    private Dictionary<Difficulty, List<EndlessPath>> pathsMap;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        pathsMap = new Dictionary<Difficulty, List<EndlessPath>>();
        List<EndlessPath> easyPaths = new List<EndlessPath>();
        List<EndlessPath> mediumPaths = new List<EndlessPath>();
        List<EndlessPath> hardPaths = new List<EndlessPath>();
        foreach (EndlessPath path in paths)
        {
            if (path.difficulty == Difficulty.EASY) easyPaths.Add(path);
            if (path.difficulty == Difficulty.MEDIUM) mediumPaths.Add(path);
            if (path.difficulty == Difficulty.HARD) hardPaths.Add(path);
        }
        pathsMap.Add(Difficulty.EASY, easyPaths);
        pathsMap.Add(Difficulty.MEDIUM, mediumPaths);
        pathsMap.Add(Difficulty.HARD, hardPaths);
    }

    public int AssignPath(Difficulty difficulty)
    {
        List<EndlessPath> list = pathsMap[difficulty];
        if (list.Count == 0)
        {
            if (difficulty == Difficulty.HARD)
                return AssignPath(Difficulty.MEDIUM);
            if (difficulty == Difficulty.MEDIUM)
            {
                int randomDifficulty = Random.Range(0, 2);
                if (randomDifficulty == 0)
                    return AssignPath(Difficulty.EASY);
                return AssignPath(Difficulty.HARD);
            }
            Debug.LogError("No easy path detected!!!");
        }
        int chosenPath = Random.Range(0, list.Count);
        return chosenPath;
    }

    public Transform GetNextWaypoint(int roadNumber, int currentWaypointIndex)
    {
        return currentWaypointIndex + 1 < paths[roadNumber].waypoints.Count ? paths[roadNumber].waypoints[currentWaypointIndex + 1] : null;
    }

    void Update()
    {

    }
}
