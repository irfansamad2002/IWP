using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CheckPoint : MonoBehaviour
{
    public static event Action<CheckPoint> OnLatestCheckpointTouch;

    public Transform playerRespawnLocation;
    [SerializeField] private BoxCollider playerHitCollider;

    AudioSource audioSource;

    bool playedSoundOnce;
    private void Awake()
    {
        playerHitCollider = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();   
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        OnLatestCheckpointTouch?.Invoke(this);

        if (!playedSoundOnce)
        {
            audioSource.Play();
            playedSoundOnce = true;
        }
    }


    private void OnDrawGizmos()
    {
        if (playerHitCollider != null)
        {
            Gizmos.color = new Color(0,0.5f,0,0.05f);
            Gizmos.DrawCube(playerHitCollider.bounds.center, playerHitCollider.size);
        }

      
        
    }
}
