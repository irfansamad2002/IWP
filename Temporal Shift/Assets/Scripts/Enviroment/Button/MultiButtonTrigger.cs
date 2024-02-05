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
    //[SerializeField] OpenWallDoor objectToBeActivated;
    [SerializeField] ActivateByMultipleButtonsEvent objectToBeActivatedNew;

    Material unLitMaterial;
    AudioSource audioSource;
    private bool isPressed;

    private bool isMakingButtonLitRoutineRunning = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        unLitMaterial = GetComponent<MeshRenderer>().material;
        objectToBeActivatedNew.OnActivation += ObjectToBeActivated_OnActivation;
    }

    private void OnDisable()
    {
        objectToBeActivatedNew.OnActivation -= ObjectToBeActivated_OnActivation;

    }

    private void ObjectToBeActivated_OnActivation()
    {
        if(!isMakingButtonLitRoutineRunning)
            StartCoroutine(MakebuttonLit(ActivationDuration));
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
        audioSource.Play();



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
