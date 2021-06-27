using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Waypoints : MonoBehaviour
{
    public static Waypoints instance;
    
    protected void Awake()
    {
        instance = this;
    }

    public abstract Transform GetNextWaypoint(int roadNumber, int currentWaypointIndex);
    protected abstract void Init();

    protected abstract void DoUpdate();

    void Start()
    {
        Init();
    }


    void Update()
    {
        DoUpdate();
    }
}
