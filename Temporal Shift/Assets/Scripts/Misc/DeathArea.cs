using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathArea : MonoBehaviour
{
    [SerializeField] Transform playerRespawnLocation;

    BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Debug.Log("Kill player");
        other.GetComponent<PlayerHealth>().KillPlayer();
    }

    private void OnDrawGizmos()
    {
        if (boxCollider != null)
        {
            Gizmos.color = new Color(0, 1, 0, 0.1f);
            Gizmos.DrawCube(boxCollider.bounds.center, boxCollider.size);
        }
    }
}
