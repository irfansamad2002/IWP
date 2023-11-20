using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDoor : MonoBehaviour
{
    public static event Action<bool> OnDoorOpenOrClose;

    [SerializeField] GameObject door;
    [SerializeField] float howHighDoorGoToOpen = 2.5f;
    [SerializeField] float doorOpenSpeed = 2f;
    private bool isOpen = false;

    float targetHowHigh;
    float originalHeight;
    private void Start()
    {
        originalHeight = transform.position.y;
        targetHowHigh = originalHeight;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        //Debug.Log("Open door");
        isOpen = true;
        targetHowHigh = howHighDoorGoToOpen + door.transform.position.y;
        OnDoorOpenOrClose?.Invoke(isOpen);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        //Debug.Log("Close door");
        isOpen = false;
        targetHowHigh = originalHeight;
        OnDoorOpenOrClose?.Invoke(isOpen);


    }

    private void Update()
    {
        Vector3 targetPos = new Vector3(door.transform.position.x, targetHowHigh, door.transform.position.z);

        door.transform.position = Vector3.Lerp(door.transform.position, targetPos, doorOpenSpeed * Time.deltaTime);
    }
}
