using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDoorActivateByLaser : MonoBehaviour
{
    public event Action OnAllGenChargedEvent;
    public event Action OnGenNotChargedEvent;
    [SerializeField] List<ChargingEmmision> allTheGenerators = new List<ChargingEmmision>();
    [SerializeField] float howLongDoorOpen = 2f;
    OpenWallDoor openWallDoor;

    private void Awake()
    {
        openWallDoor = GetComponent<OpenWallDoor>();
    }


    private bool IfAllGeneratorCharged()
    {
        bool allGeneratorCharged = true;

        foreach (ChargingEmmision eachGenerator in allTheGenerators)
        {
            if (!eachGenerator.IsCharged())
            {
                allGeneratorCharged = false;
                break;
            }
        }
        return allGeneratorCharged;

    }

    public void OpenDoor()
    {
        openWallDoor.Activate(howLongDoorOpen);
    }



    private void Update()
    {
        if (IfAllGeneratorCharged())
        {
            OpenDoor();
        }
       
    }
}
