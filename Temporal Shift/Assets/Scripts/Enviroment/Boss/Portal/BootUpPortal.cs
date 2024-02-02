using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootUpPortal : MonoBehaviour
{
    public static event Action onPortalOpenEvent;
    [SerializeField] GameObject PortalEffectGo;
    [SerializeField] BoxCollider boxColliderGo;
    [SerializeField] List<ChargingEmmision> allTheGenerators = new List<ChargingEmmision>();
    private bool stopChecking;

    private void Start()
    {
        TurnOff();
        boxColliderGo.enabled = false;

    }
    private bool IfAllGeneratorCharged()
    {
        bool allGeneratorCharged = true;

        foreach (ChargingEmmision eachGenerator in allTheGenerators)
        {
            if (!eachGenerator.IsCharged())
            {
                allGeneratorCharged = false;
                break;
            }
        }
        return allGeneratorCharged;

    }

   
    [ContextMenu("Turn the portal on")]
    public void TurnOn()
    {
        PortalEffectGo.SetActive(true);
        stopChecking = true;
        boxColliderGo.enabled = true;
        onPortalOpenEvent?.Invoke();

    }

    void TurnOff()
    {
        PortalEffectGo.SetActive(false);

    }

    private void Update()
    {
        if (stopChecking)
            return;
        if (IfAllGeneratorCharged())
        {
            Debug.Log("Turn on portal, all " + allTheGenerators.Count + " of the generator is charged");
            TurnOn();
            stopChecking = true;
        }
        else
        {
            TurnOff();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("END GAME");
            SceneManager.LoadScene(2);

        }
    }
}
