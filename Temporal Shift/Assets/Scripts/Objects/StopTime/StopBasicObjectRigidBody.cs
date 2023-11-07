using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBasicObjectRigidBody : MonoBehaviour, IStopTimeable
{
    Rigidbody rb;

    private Vector3 storedVelocity, storedAngularVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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

        storedVelocity = rb.velocity;
        storedAngularVelocity = rb.angularVelocity;
        rb.isKinematic = true;  
        

    }
}
