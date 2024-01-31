using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHitable
{
    public static event Action OnHit;
    public static event Action OnDeath;

    [Header("how many time player can get shot by turret")]
    public int Maxhealth = 3;
    [HideInInspector]
    public int currentHealth;
    private void Start()
    {
        currentHealth = Maxhealth;
    }

    private void OnEnable()
    {
        RespawnManager.OnRespawnEvent += RespawnManager_OnRespawnEvent;

    }

    

    private void OnDisable()
    {
        RespawnManager.OnRespawnEvent -= RespawnManager_OnRespawnEvent;

    }

    public void Hit()
    {
        currentHealth--;
        OnHit?.Invoke();

        Debug.Log("got hit, now health is " + currentHealth);
    }

    public void Hit(int damage)
    {
        currentHealth -= damage;
        OnHit?.Invoke();

        Debug.Log("got hit, now health is " + currentHealth);

    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        Debug.Log("player died");
        GetComponent<LookAroundWithMouse>().enabled = false;
        OnDeath?.Invoke();
        currentHealth = Maxhealth;

    }

    private void RespawnManager_OnRespawnEvent()
    {
        GetComponent<LookAroundWithMouse>().enabled = true;

    }
}
