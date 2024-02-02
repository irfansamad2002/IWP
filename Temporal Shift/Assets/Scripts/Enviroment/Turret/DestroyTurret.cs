using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTurret : MonoBehaviour
{
    [SerializeField] BossParent bossParent;
    ActivateByMultipleButtonsEvent buttonsEvent;
    
    private void Awake()
    {
        buttonsEvent = GetComponent<ActivateByMultipleButtonsEvent>();
    }


    private void OnEnable()
    {
        buttonsEvent.OnActivation += ButtonsEvent_OnActivation;
        BossParent.OnPhaseTwoEvent += BossParent_OnPhaseTwoEvent;
    }

    private void OnDisable()
    {
        buttonsEvent.OnActivation -= ButtonsEvent_OnActivation;
        BossParent.OnPhaseTwoEvent -= BossParent_OnPhaseTwoEvent;

    }

    private void ButtonsEvent_OnActivation()
    {
        Debug.Log("Destroy this turret");
        bossParent.TurretDestroyed();
        Destroy(gameObject);
    }

    private void BossParent_OnPhaseTwoEvent()
    {
        Destroy(gameObject);
    }
}
