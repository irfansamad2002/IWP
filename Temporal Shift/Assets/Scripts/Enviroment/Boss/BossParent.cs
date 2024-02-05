using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossParent : MonoBehaviour
{
    public static event Action OnPhaseTwoEvent;

    private RotateObject currentRewindingObject;

    [SerializeField] SpawnWall AtkPatternA;
    [SerializeField] SpawnWall AtkPatternB;
    [SerializeField] float attackCooldown = 10f;
    [SerializeField] List<GameObject> turrets = new List<GameObject>();
    [SerializeField] AudioSource audioSource;
    public bool isStillAttacking = true;

    bool checkedForPhaseTwo;
    int amountOfTurrets;


    private void Start()
    {
        amountOfTurrets = turrets.Count;
    }

    private void OnEnable()
    {
        RotateObject.OnRewindStarted += RotateObject_OnRewindStarted;
        RotateObject.OnRewindStopped += RotateObject_OnRewindStopped;
        OnPhaseTwoEvent += BossParent_OnPhaseTwoEvent;
        BootUpPortal.onPortalOpenEvent += BootUpPortal_onPortalOpenEvent;
    }

  
    private void OnDisable()
    {
        RotateObject.OnRewindStarted -= RotateObject_OnRewindStarted;
        RotateObject.OnRewindStopped -= RotateObject_OnRewindStopped;
        OnPhaseTwoEvent -= BossParent_OnPhaseTwoEvent;

        BootUpPortal.onPortalOpenEvent -= BootUpPortal_onPortalOpenEvent;
    }


    private void RotateObject_OnRewindStarted(RotateObject rotatingObj)
    {
        //nother object is already rewinding, stop it
        if (currentRewindingObject != null && currentRewindingObject != rotatingObj)
        {
            currentRewindingObject.StopRewinding();
        }

        // Set the current rewinding object
        currentRewindingObject = rotatingObj;
    }

    private void RotateObject_OnRewindStopped(RotateObject rotatingObj)
    {
        // Reset the current rewinding object
        if (currentRewindingObject == rotatingObj)
        {
            currentRewindingObject = null;
        }
    }

  

    IEnumerator AttackSequence()
    {
        while (isStillAttacking) // Infinite loop for continuous attacks
        {
            AtkPatternA.AtkStart();

            // Wait for some time
            yield return new WaitForSeconds(attackCooldown); // Adjust the time as needed

            AtkPatternB.AtkStart();

            // Wait for some time before repeating the sequence
            yield return new WaitForSeconds(attackCooldown); // Adjust the time as needed
        }
    }

    [ContextMenu("Start boss atk")]
    public void StartBossAttack()
    {
        isStillAttacking = true;
        StartCoroutine(AttackSequence());

    }

    [ContextMenu("Stop boss atk")]
    public void StopBossAttack()
    {
        isStillAttacking = false;
    }

    private void BossParent_OnPhaseTwoEvent()
    {
        audioSource.Play();
        StartBossAttack();
    }

    public void TurretDestroyed()
    {
        amountOfTurrets--;
        Debug.Log(amountOfTurrets);
        if (amountOfTurrets <= 0)
        {
            OnPhaseTwoEvent?.Invoke();
        }
        //
    }

    public void ForcePhaseTwo()
    {
        OnPhaseTwoEvent?.Invoke();
    }

    private void BootUpPortal_onPortalOpenEvent()
    {
        StopBossAttack();
    }


}
