using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundForGenerator : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip chargingUpSFX, chargedSFX, unchargingSFX;

    ChargingEmmision chargingEmmision;

    private void Awake()
    {
        chargingEmmision = GetComponent<ChargingEmmision>();
    }

    private void OnEnable()
    {
        chargingEmmision.onChargeEvent += ChargingEmmision_onChargeEvent;
        chargingEmmision.onUnchargeEvent += ChargingEmmision_onUnchargeEvent;
    }

   
    private void OnDisable()
    {
        chargingEmmision.onChargeEvent -= ChargingEmmision_onChargeEvent;
        chargingEmmision.onChargeEvent -= ChargingEmmision_onChargeEvent;
    }

    private void ChargingEmmision_onUnchargeEvent()
    {
        audioSource.PlayOneShot(unchargingSFX);

    }

    private void ChargingEmmision_onChargeEvent()
    {
        audioSource.PlayOneShot(chargingUpSFX);
    }



}
