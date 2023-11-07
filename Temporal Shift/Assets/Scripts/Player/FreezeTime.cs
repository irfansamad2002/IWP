using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class FreezeTime : MonoBehaviour
{
    public event Action OnAbilityPressed;

    [SerializeField] private InputReader inputReader;
    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private GameObject freezeOrb;

    [SerializeField] private Transform spawnPos;
    private Vector3 aimTransform;

    public bool AbleToShoot;
    private void OnEnable()
    {
        inputReader.AbilityEvent += HandleEvent;  
        OnAbilityPressed += HandleOnAbilityPressed;
    }

    private void OnDisable()
    {
        inputReader.AbilityEvent -= HandleEvent;
        OnAbilityPressed -= HandleOnAbilityPressed;
    }

    private void Awake()
    {
        AbleToShoot = true;
    }

    private void HandleEvent(bool state)
    {
        //Debug.Log(state);
        if (state)
        {
            OnAbilityPressed?.Invoke();
        }
    }

    private void HandleOnAbilityPressed()
    {
        if (!AbleToShoot)
            return;
        aimTransform = vCam.Follow.transform.forward;
       
        //Instantiate
        Debug.Log("Spawn FreezeOrb");
        GameObject go = Instantiate(freezeOrb, spawnPos.position, Quaternion.identity);
        go.GetComponent<FreezeOrb>().Init(this, spawnPos.position, aimTransform);
        AbleToShoot = false;


    }
}
