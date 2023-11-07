using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopWaypointsObjectMovement : MonoBehaviour, IStopTimeable
{
    public Transform[] Waypoints;
    public float speed = 5.0f;
    public float waypointThreshold = 0.1f;//Threshold for considering a waypoint as reached

    private int currrentWaypoint = 0;
    private bool isMoving = true;

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
            MoveThisObject();
    }

    private void MoveThisObject()
    {
        // Check if there are waypoints
        if (Waypoints.Length == 0)
            return;

        // Move towards the current waypoint
        transform.position = Vector3.MoveTowards(transform.position, Waypoints[currrentWaypoint].position, speed * Time.deltaTime);

        // Check if the platform has reached the current waypoint
        if (Vector3.Distance(transform.position, Waypoints[currrentWaypoint].position) < waypointThreshold)
        {
            currrentWaypoint++;

            // If the platform reaches the end of the waypoint list, loop back to the first waypoint
            if (currrentWaypoint >= Waypoints.Length)
            {
                currrentWaypoint = 0;
            }
        }
    }

    public void StopMoving()
    {
        isMoving = false;
    }

    public void StartMoving()
    {
        isMoving = true;
    }
}
