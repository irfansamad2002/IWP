using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    [SerializeField] float maxDistance = 5f;

    private InputReader inputReader;

    private void Awake()
    {
        inputReader = GetComponent<Movement>().inputReader;
    }
    private void OnEnable()
    {
        inputReader.InteractEvent += InputReader_InteractEvent;
    }

   

    private void OnDisable()
    {
        inputReader.InteractEvent -= InputReader_InteractEvent;
    }

    private void InputReader_InteractEvent()
    {
        CheckObjectToInteract();

    }



    void CheckObjectToInteract()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, maxDistance))
        {
            IInteractable iInteractable = hit.collider.gameObject.GetComponent<IInteractable>();

            if (iInteractable != null)
            {
                iInteractable.Interacted();
            }
        }
    }

}
