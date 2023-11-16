using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathArea : MonoBehaviour
{
    public static event Action<Transform> OnRespawnPlayer;

    [SerializeField] Transform playerRespawnLocation;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Debug.Log("Kill player");
        OnRespawnPlayer?.Invoke(playerRespawnLocation);
    }
}
