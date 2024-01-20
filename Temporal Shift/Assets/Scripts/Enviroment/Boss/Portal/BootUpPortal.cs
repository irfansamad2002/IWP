using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootUpPortal : MonoBehaviour
{
    [SerializeField] GameObject PortalEffectGo;
    [SerializeField] List<ChargingEmmision> allTheGenerators = new List<ChargingEmmision>();


    private void Start()
    {
        TurnOff();


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
    void TurnOn()
    {
        PortalEffectGo.SetActive(true);


    }

    void TurnOff()
    {
        PortalEffectGo.SetActive(false);

    }

    private void Update()
    {
        if (IfAllGeneratorCharged())
        {
            Debug.Log("Turn on portal, all " + allTheGenerators.Count + " of the generator is charged");
            TurnOn();
        }
        else
        {
            TurnOff();
        }
    }

}
