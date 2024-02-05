using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class ChargingEmmision : MonoBehaviour
{
    public event Action onChargeEvent;
    public event Action onUnchargeEvent;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] float maxEmission = 500f;
    [SerializeField] float lerpSpeed = 2f;
    [SerializeField] float afterSomeTimeStopChargeDuration = 1f;
    public Light genLight;

    private bool isCharge;
    private float litEmission = 1f;
    public Color generatorColor;

    private float targetEmission;
    private float currentEmission;

    bool checkedCharge, checkedUnCharge;


    private void Start()
    {
        // Initialize target and current emission to the default value
        targetEmission = 1f;
        currentEmission = 1f;
        genLight.enabled = false;
    }

    [ContextMenu("Charged")]
    public void Charge()
    {
        isCharge = true;
        CancelInvoke();

        // Set the target emission to the maximum value when charged
        targetEmission = maxEmission;
        genLight.enabled = true;
        Invoke(nameof(StopCharge), afterSomeTimeStopChargeDuration);
    }

    [ContextMenu("Stop Charge")]
    public void StopCharge()
    {
        isCharge = false;

        targetEmission = 1f;
        genLight.enabled = false;

    }


    private void Update()
    {
        // Lerp towards the target emission value
        currentEmission = Mathf.Lerp(currentEmission, targetEmission, Time.deltaTime * lerpSpeed);

        // Set the emission color to white (you can change this to any color)
        Color finalColor = generatorColor * Mathf.LinearToGammaSpace(currentEmission);

        meshRenderer.material.SetColor("_EmissionColor", finalColor);

        if (isCharge && !checkedCharge)
        {
            onChargeEvent?.Invoke();
            checkedCharge = true;
            checkedUnCharge = false;
        }

        if (!isCharge && !checkedUnCharge)
        {
            onUnchargeEvent?.Invoke();


            checkedUnCharge = true;
            checkedCharge = false;
        }
    }

    public bool IsCharged()
    {
        return isCharge;
    }

}
