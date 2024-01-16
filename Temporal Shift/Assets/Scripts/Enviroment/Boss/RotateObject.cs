using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour, IRewindTimeable
{
    public float RotateSpeed = 35f;

    private int direction = 1;

    private bool isRewinding;
    

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up, RotateSpeed * direction * Time.deltaTime); 
    }

    public void StartRewinding()
    {
        if (!isRewinding)
        {
            direction = -direction;
            isRewinding = true;
        }

    }

    public void StopRewinding()
    {
        direction = -direction;
        isRewinding = false;

    }

    public void AbleToRewind()
    {

    }
}
