using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBasicObjectRigidBody : MonoBehaviour, IStopTimeable
{
    Rigidbody rb;

    private Vector3 storedVelocity, storedAngularVelocity;
    LoopWaypointsObjectMovement directionWaypoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        directionWaypoint = GetComponentInChildren<LoopWaypointsObjectMovement>();
    }
    public void StartMoving()
    {
        Debug.Log("Start");

        rb.isKinematic = false;
        rb.velocity = storedVelocity;
        rb.angularVelocity = storedAngularVelocity;
    }

    public void StopMoving()
    {
        Debug.Log("STOP");

        directionWaypoint.DirectionToWaypoint = Vector3.zero;
        storedVelocity = rb.velocity;
        storedAngularVelocity = rb.angularVelocity;
        rb.isKinematic = true;  
        

    }
}
