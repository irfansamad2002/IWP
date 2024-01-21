using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TimeBody))]
public class LoopWaypointsObjectMovement : MonoBehaviour, IStopTimeable, ISpeedTimeable
{
    public Transform[] Waypoints;
    
    public float speed = 5.0f;
    public float waypointThreshold = 0.1f;//Threshold for considering a waypoint as reached

    private int currrentWaypoint = 0;
    private bool isMoving = true;

    private TimeBody timeBody;
    private StopObjectAnimation objectAnimationState;

    private float defaultSpeed;

    [Header("For External Motion")]
    public Vector3 DirectionToWaypoint;

    private bool delayedBeforeTurn;
    //[SerializeField] private float delayDuration = 1f;

    private void Awake()
    {
        timeBody = GetComponent<TimeBody>();
        objectAnimationState = GetComponent<StopObjectAnimation>();
    }

    private void Start()
    {
        defaultSpeed = speed;
        delayedBeforeTurn = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (timeBody.isRewinding)
            return;
        if (!isMoving)
            return;
        
        //if (delayedBeforeTurn)
        //{
            MoveThisObject();
        //}


    }

    private void MoveThisObject()
    {
        
        // Check if there are waypoints
        if (Waypoints.Length == 0)
            return;

        // Move towards the current waypoint
        transform.position = Vector3.MoveTowards(transform.position, Waypoints[currrentWaypoint].position, speed * Time.deltaTime);

        DirectionToWaypoint = Waypoints[currrentWaypoint].position - transform.position;

        DirectionToWaypoint.Normalize();

        // Check if the platform has reached the current waypoint
        if (Vector3.Distance(transform.position, Waypoints[currrentWaypoint].position) < waypointThreshold)
        {
            
            currrentWaypoint++;
            StartCoroutine(delayCoroutine(1f));
            // If the platform reaches the end of the waypoint list, loop back to the first waypoint
            if (currrentWaypoint >= Waypoints.Length)
            {
                currrentWaypoint = 0;
            }
        }
    }

    private IEnumerator delayCoroutine(float duration)
    {

        delayedBeforeTurn = false;
        yield return new WaitForSeconds(duration);
        delayedBeforeTurn = true;
    }

    public void StopMoving()
    {
        isMoving = false;
        DirectionToWaypoint = Vector3.zero;

        if (objectAnimationState != null)
            objectAnimationState.StopAnimating();
    }

    public void StartMoving()
    {
        isMoving = true;
        if (objectAnimationState != null)
            objectAnimationState.StartAnimating();

    }

    public void MoveFastSpeed(float scale)
    {
        speed *= scale;
    }

    public void MoveNormalSpeed()
    {
        speed = defaultSpeed;
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.yellow;

        if (Waypoints[0] == null)
            return;

        for (int i = 0; i < Waypoints.Length - 1; i++)
        {
            Gizmos.DrawLine(Waypoints[i].position, Waypoints[i + 1].position);
        }

        // Draw a line from the last waypoint to the first waypoint to complete the loop
        if (Waypoints.Length > 1)
        {
            Gizmos.DrawLine(Waypoints[Waypoints.Length - 1].position, Waypoints[0].position);
        }


    }
}
