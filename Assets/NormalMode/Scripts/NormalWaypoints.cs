using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalWaypoints : Waypoints
{
    [SerializeField]
    public List<Path> paths;

    protected void Awake()
    {
        instance = this;
    }

    public override Transform GetNextWaypoint(int roadNumber, int currentWaypointIndex)
    {
        return currentWaypointIndex + 1 < paths[roadNumber].waypoints.Count ? paths[roadNumber].waypoints[currentWaypointIndex + 1] : null;
    }

    protected override void Init()
    {
        //not needed
    }

    protected override void DoUpdate()
    {
        //not needed
    }
}
