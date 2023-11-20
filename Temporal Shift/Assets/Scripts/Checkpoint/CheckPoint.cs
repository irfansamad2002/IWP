using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CheckPoint : MonoBehaviour
{
    public static event Action<CheckPoint> OnLatestCheckpointTouch;

    public Transform playerRespawnLocation;
    [SerializeField] private BoxCollider playerHitCollider;

    private void Awake()
    {
        playerHitCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        OnLatestCheckpointTouch?.Invoke(this);
    }


    private void OnDrawGizmos()
    {
        if (playerHitCollider != null)
        {
            Gizmos.color = new Color(0,1,0,0.1f);
            Gizmos.DrawCube(playerHitCollider.bounds.center, playerHitCollider.size);
        }

      
        
    }
}
