using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeOrb : MonoBehaviour
{
    public static event Action OnStopOrb;
    public static event Action OnDestroyOrb;

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] InputReader _inputReader;
    [SerializeField] private float _howLongTheOrbLast = 5f;
    private Vector3 _fireDirection;
    private Rigidbody _rigidBody;

    [HideInInspector] public bool ableToMove;
    private FreezeTime _freezeTime;

    private List<IStopTimeable> stoppedObjects = new List<IStopTimeable>();

    private void OnEnable()
    {
        _inputReader.AbilityEvent += HandlePressedAbility;
    }

    private void OnDisable()
    {
        _inputReader.AbilityEvent -= HandlePressedAbility;

    }

    private void HandlePressedAbility(bool state)
    {
        if (state == false)
            return;

        //Debug.Log("Left Click check on FreezeOrb");

        //if user left click, destroy the orb
        if (!ableToMove)
        {
            DespawnTheOrb();
        }

        //if user left click, stop the orb from moving
        if (!_freezeTime.AbleToShoot)
        {
            OnStopOrb?.Invoke();
            ableToMove = false;
        }

        
    }

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        IStopTimeable iStopTimeable = other.GetComponent<IStopTimeable>();
        if (iStopTimeable != null)
        {
            iStopTimeable.StopMoving();
            stoppedObjects.Add(iStopTimeable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IStopTimeable iStopTimeable = other.GetComponent<IStopTimeable>();
        if (iStopTimeable != null)
        {
            iStopTimeable.StartMoving();
            stoppedObjects.Remove(iStopTimeable);
        }
    }

    

    public void Init(FreezeTime freezeTime, Vector3 spawnPos, Vector3 aimPointing)
    {
        this._freezeTime = freezeTime;
        _fireDirection = aimPointing;
        ableToMove = true;
        Utils.RunAfterDelay(this, _howLongTheOrbLast, DespawnTheOrb);
    }

    private void DespawnTheOrb()
    {
        OnDestroyOrb?.Invoke();


        _freezeTime.AbleToShoot = true;

        // Notify the objects inside the orb to resume their movement
        foreach (var stoppedObject in stoppedObjects)
        {
            stoppedObject.StartMoving();
        }

        GameObject.Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        if (ableToMove)
        {
            _rigidBody.velocity = _fireDirection * _moveSpeed;
        }
        else
        {
            _rigidBody.velocity = Vector3.zero;
        }
    }
}
