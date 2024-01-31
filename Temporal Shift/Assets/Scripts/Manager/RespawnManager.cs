using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] GameObject player;

    private CheckPoint latestActivatedCheckpoint;
    public Transform defaultRespawnLocation;

    public static event Action OnRespawnEvent;

   

    private void OnEnable()
    {
        CheckPoint.OnLatestCheckpointTouch += CheckPoint_OnLatestCheckpointTouch;
    }

    private void OnDisable()
    {
        CheckPoint.OnLatestCheckpointTouch -= CheckPoint_OnLatestCheckpointTouch;
    }

  

    public void AutoRespawnPlayerLatestCheckpoint()
    {
        if (latestActivatedCheckpoint == null)
        {
            Debug.Log("RESPAWN AT" + defaultRespawnLocation);
            player.GetComponent<Movement>().enabled = false;
            player.transform.position = defaultRespawnLocation.position;
            Invoke("makePlayerMOVELASIAL", 0.1f);
        }
        else
        {
            Debug.Log("RESPAWN AT" + latestActivatedCheckpoint.playerRespawnLocation);
            player.GetComponent<Movement>().enabled = false;
            player.transform.position = latestActivatedCheckpoint.playerRespawnLocation.position;
            Invoke("makePlayerMOVELASIAL", 0.1f);
        }

        OnRespawnEvent?.Invoke();



    }

    public void AutoRespawnPlayerToStart()
    {
        Debug.Log("RESPAWN AT" + defaultRespawnLocation);
        player.GetComponent<Movement>().enabled = false;
        player.GetComponent<LookAroundWithMouse>().enabled = false;
        player.transform.position = defaultRespawnLocation.position;
        Invoke("makePlayerMOVELASIAL", 0.1f);
      
        OnRespawnEvent?.Invoke();
    }
    private void CheckPoint_OnLatestCheckpointTouch(CheckPoint checkpoint)
    {
        latestActivatedCheckpoint = checkpoint;
    }

    private void makePlayerMOVELASIAL()
    {
        player.GetComponent<Movement>().enabled = true;

    }

}
