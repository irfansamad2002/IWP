using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlatform : MonoBehaviour
{
    public List<Transform> Positions = new List<Transform>();
    public float moveSpeed = 0.5f;
    public bool MoveForwardDirection = true;
    public float currentLerpIndex = 0f;

    public bool MoveBackAndForth = false;


    private Vector3 _initialPosition;
    private int _currentTargetIndex = 0;
    private int directionInt;

    private void Start()
    {
        _initialPosition = transform.position;
        directionInt = MoveForwardDirection ? 1 : -1;
    }


    private void Update()
    {
        directionInt = MoveForwardDirection ? 1 : -1;

        //Increment currentLerpIndex over time
        currentLerpIndex += moveSpeed * directionInt * Time.deltaTime;
        currentLerpIndex = Mathf.Clamp01(currentLerpIndex);

        Vector3 nextPosition = Positions[_currentTargetIndex].position;
        //transform.position = Vector3.MoveTowards(transform.position, nextPosition, (moveSpeed * directionInt) * Time.deltaTime);

        transform.position = Vector3.Lerp(_initialPosition, nextPosition, currentLerpIndex);

        if (MoveBackAndForth)
        {
            if (currentLerpIndex == 0f || currentLerpIndex == 1f)
            {
                MoveForwardDirection = !MoveForwardDirection;
                Debug.Log("change direction");
            }
        }


    }
}
