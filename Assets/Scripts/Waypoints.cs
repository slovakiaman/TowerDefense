﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Waypoints instance;
    
    [SerializeField]
    public List<Path> paths;

    private void Awake()
    {
        instance = this;
    }

    public Transform GetNextWaypoint(int roadNumber, int currentWaypointIndex)
    {
        return currentWaypointIndex + 1 < paths[roadNumber].waypoints.Count ? paths[roadNumber].waypoints[currentWaypointIndex + 1] : null;
    }

    void Start()
    {

    }

    void Update()
    {
        
    }
}
