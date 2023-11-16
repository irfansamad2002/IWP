using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{

    [SerializeField] GameObject Player;

    private void OnEnable()
    {
        DeathArea.OnRespawnPlayer += DeathArea_OnRespawnPlayer;
    }

    private void OnDisable()
    {
        DeathArea.OnRespawnPlayer -= DeathArea_OnRespawnPlayer;
    }

    private void DeathArea_OnRespawnPlayer(Transform respawnLocation)
    {
        Debug.Log("RESPAWN AT" + respawnLocation.position);
        Player.GetComponent<Movement>().enabled = false;
        Player.transform.position = respawnLocation.position;
        Invoke("makePlayerMOVELASIAL", 0.1f);

    }

    private void makePlayerMOVELASIAL()
    {
        Player.GetComponent<Movement>().enabled = true;

    }

}
