using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialBaseOnBoolean : MonoBehaviour
{
    [SerializeField] MultiButtonTrigger multiButtonTrigger;

    Material defaultMaterial;
    MeshRenderer meshRenderer;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        defaultMaterial = meshRenderer.material;
    }

    private void Update()
    {
        if (multiButtonTrigger.IsPressed())
        {
            meshRenderer.material = multiButtonTrigger.gameObject.GetComponent<MeshRenderer>().material;
        }
        else
        {
            meshRenderer.material = defaultMaterial;
        }
    }
}
