using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour, ISpeedTimeable
{
    public float moveSpeed = 1.0f; // Adjust this value to control the movement speed.
    private Rigidbody rb;
    private float initialMoveSpeed;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        initialMoveSpeed = moveSpeed;
    }


    private void FixedUpdate()
    {
        Vector3 moveDirection = transform.forward * moveSpeed;

        rb.AddForce(moveDirection, ForceMode.VelocityChange);
    }

    public void MoveFastSpeed(float scale)
    {
        moveSpeed *= scale;
    }

    public void MoveNormalSpeed()
    {
        moveSpeed = initialMoveSpeed;
    }
}
