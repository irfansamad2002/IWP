using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class ChargingEmmision : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] float maxEmission = 500f;
    [SerializeField] float lerpSpeed = 2f;
    [SerializeField] float afterSomeTimeStopChargeDuration = 1f;

    private bool isCharge;
    private float litEmission = 1f;
    public Color generatorColor;

    private float targetEmission;
    private float currentEmission;

    private void Start()
    {
        // Initialize target and current emission to the default value
        targetEmission = 1f;
        currentEmission = 1f;
    }

    [ContextMenu("Charged")]
    public void Charge()
    {
        isCharge = true;
        CancelInvoke();

        // Set the target emission to the maximum value when charged
        targetEmission = maxEmission;

        Invoke(nameof(StopCharge), afterSomeTimeStopChargeDuration);
    }

    [ContextMenu("Stop Charge")]
    public void StopCharge()
    {
        isCharge = false;

        targetEmission = 1f;
    }


    private void Update()
    {
        // Lerp towards the target emission value
        currentEmission = Mathf.Lerp(currentEmission, targetEmission, Time.deltaTime * lerpSpeed);

        // Set the emission color to white (you can change this to any color)
        Color finalColor = generatorColor * Mathf.LinearToGammaSpace(currentEmission);

        meshRenderer.material.SetColor("_EmissionColor", finalColor);
    }

    public bool IsCharged()
    {
        return isCharge;
    }

}
