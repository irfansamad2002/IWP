using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPositionVector3 : MonoBehaviour
{
    
    public Vector3 InitialPosition { get; private set; }

    private void Awake()
    {
        InitialPosition = transform.position;
    }
}
