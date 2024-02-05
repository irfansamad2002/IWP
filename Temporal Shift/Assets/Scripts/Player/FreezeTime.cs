using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class FreezeTime : MonoBehaviour
{
    public static event Action OnSpawnFreezeOrb;

    [SerializeField] private InputReader inputReader;
    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private GameObject freezeOrb;

    [SerializeField] private Transform spawnPos;
    private Vector3 aimTransform;

    public bool AbleToShoot;
    private void OnEnable()
    {
        inputReader.AbilityEvent += HandleEvent;  
    }

    private void OnDisable()
    {
        inputReader.AbilityEvent -= HandleEvent;
    }

    private void Awake()
    {
        AbleToShoot = true;
    }

    private void HandleEvent(bool state)
    {
        if (UIManager.Instance.isPausing)
            return;

        
        if (state)
        {
            if (!AbleToShoot)
                return;
            aimTransform = vCam.Follow.transform.forward;

            //Instantiate
            //Debug.Log("Spawn FreezeOrb");
            OnSpawnFreezeOrb?.Invoke();
            GameObject go = Instantiate(freezeOrb, spawnPos.position, Quaternion.identity);
            go.GetComponent<FreezeOrb>().Init(this, spawnPos.position, aimTransform);
            AbleToShoot = false;
        }
    }

  
}
