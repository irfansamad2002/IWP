using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAudio : MonoBehaviour
{
    [SerializeField] WallDoorActivateByLaser wallDoor;

    bool checkedOpen;
    bool checkedClose;

    private void OnEnable()
    {
        wallDoor.OnAllGenChargedEvent += WallDoor_OnAllGenChargedEvent;
        wallDoor.OnGenNotChargedEvent += WallDoor_OnGenNotChargedEvent;
    }

    private void WallDoor_OnGenNotChargedEvent()
    {
        Debug.Log("NOT CGARGE");
    }

    private void WallDoor_OnAllGenChargedEvent()
    {
        Debug.Log(" CGARGE");

    }

    private void OnDisable()
    {
        wallDoor.OnAllGenChargedEvent -= WallDoor_OnAllGenChargedEvent;
        wallDoor.OnGenNotChargedEvent -= WallDoor_OnGenNotChargedEvent;
    }


}
