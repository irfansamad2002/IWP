using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] float frequency = 0.5f; // Adjust the cooldown time as needed

    [Header("Player Footstep")]
    [SerializeField] AudioClip footStepSFX;
    [Header("Player Jump Start")]
    [SerializeField] AudioClip jumpStartSFX;
    [Header("Player Jump Land")]
    [SerializeField] AudioClip jumpLandSFX;

    private float lastPlayTime;

    bool isMoving;
    bool isJumping;

    bool playedJumpSFX;
    

    JumpAndGravity JandG;
    Movement movement;
    private void Awake()
    {
        JandG = GetComponent<JumpAndGravity>();
        movement = GetComponent<Movement>();
    }

    private void Start()
    {
        lastPlayTime = -frequency;
    }

    private void Update()
    {
        if (isJumping && JandG.Grounded)
        {
            Debug.Log("play land");
            audioSource.clip = jumpLandSFX;
            audioSource.PlayOneShot(jumpLandSFX);
            
        }
        if (JandG.Grounded)
        {
            isJumping = false;
            
            playedJumpSFX = false;
        }
        else
        {
            isJumping = true;
        }

        if (movement._inputDirection.magnitude <= 0.01f)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }

        
        //player moving normal
        if (isMoving && !isJumping)
        {
            if (Time.time - lastPlayTime >= frequency)
            {
                audioSource.clip = footStepSFX;
                audioSource.Play();
                lastPlayTime = Time.time;
            }
        }

        //player jump
        if (isJumping && !playedJumpSFX)
        {
            audioSource.clip = jumpStartSFX;
            audioSource.Play();
            playedJumpSFX = true;
        }


    }

}
