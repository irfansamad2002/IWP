using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class MultiButtonTrigger : MonoBehaviour, IHitable, IInteractable
{
    [SerializeField] float ActivationDuration = 2f;

    [Space(5)]
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Material litMaterial;
    [SerializeField] Material pendingMaterial;

    [Space(5)]
    [SerializeField] ActivateByMultipleButtons objectToBeActivated;

    Material unLitMaterial;
    private bool isPressed;

    private bool isMakingButtonLitRoutineRunning = false;

    private void OnEnable()
    {
        unLitMaterial = GetComponent<MeshRenderer>().material;
        objectToBeActivated.OnActivation += ObjectToBeActivated_OnActivation;
    }

    private void OnDisable()
    {
        objectToBeActivated.OnActivation -= ObjectToBeActivated_OnActivation;

    }

    private void ObjectToBeActivated_OnActivation(float howLong)
    {
        if(!isMakingButtonLitRoutineRunning)
            StartCoroutine(MakebuttonLit(howLong));
    }


    public void Hit()
    {
        PressButton();
    }

    public void Hit(int damage)
    {

    }

    public void Interacted()
    {
        PressButton();
    }

    public void PressButton()
    {
        if (isMakingButtonLitRoutineRunning)
            return;

        isPressed = true;
        meshRenderer.material = pendingMaterial;
        CancelInvoke("ResetButton");
        Invoke("ResetButton", ActivationDuration); 

       
    }

    private void ResetButton()
    {
        isPressed = false;
        meshRenderer.material = unLitMaterial;
    }

    public bool IsPressed()
    {
        return isPressed;
    }

    public IEnumerator MakebuttonLit(float howLong)
    {
        isMakingButtonLitRoutineRunning = true;
        if (isPressed)
        {
            CancelInvoke("ResetButton");

            meshRenderer.material = litMaterial;

            yield return new WaitForSeconds(howLong);

            ResetButton();
        }

        isMakingButtonLitRoutineRunning = false;
    }
}
