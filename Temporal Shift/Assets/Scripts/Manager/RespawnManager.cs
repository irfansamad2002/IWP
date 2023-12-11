using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{

    GameObject player;

    private CheckPoint latestActivatedCheckpoint;

    public Transform defaultRespawnLocation;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        PlayerHealth.OnDeath += PlayerHealth_OnDeath;
        CheckPoint.OnLatestCheckpointTouch += CheckPoint_OnLatestCheckpointTouch;
    }

    private void OnDisable()
    {
        PlayerHealth.OnDeath -= PlayerHealth_OnDeath;
        CheckPoint.OnLatestCheckpointTouch -= CheckPoint_OnLatestCheckpointTouch;
    }

    private void RespawnPlayer(Transform respawnLocation)
    {
        Debug.Log("RESPAWN AT" + respawnLocation.position);
        player.GetComponent<Movement>().enabled = false;
        player.transform.position = respawnLocation.position;
        Invoke("makePlayerMOVELASIAL", 0.1f);

    }

    private void PlayerHealth_OnDeath()
    {
        if (latestActivatedCheckpoint == null)
        {
            RespawnPlayer(defaultRespawnLocation);
        }
        else
        {
            RespawnPlayer(latestActivatedCheckpoint.playerRespawnLocation);
        }
    }

    private void CheckPoint_OnLatestCheckpointTouch(CheckPoint checkpoint)
    {
        latestActivatedCheckpoint = checkpoint;
        //Debug.Log("latest place the player shd respawn is at " + checkpoint.playerRespawnLocation.position);
    }

    private void makePlayerMOVELASIAL()
    {
        player.GetComponent<Movement>().enabled = true;

    }

}
