using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour, IHitable, IInteractable
{

    [SerializeField] float ActivationDuration = 2f;
    
    [Space(5)]
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Material litMaterial;


    Material unLitMaterial;
    
    public List<GameObject> objectsActivatedByButton = new List<GameObject>();

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        unLitMaterial = GetComponent<MeshRenderer>().material;
    }

    public void Hit(int damage)
    {

    }

    public void Hit()//if bullet hit button
    {
        OnInteracted();
    }
    public void Interacted()//if player interact button
    {
        OnInteracted();
    }

    private void OnInteracted()
    {
        audioSource.Play();
        StartCoroutine(LitButtonEnumerator(ActivationDuration));
        foreach (GameObject eachObjectToActivate in objectsActivatedByButton)
        {
            IButtonActivation activateViaButton = eachObjectToActivate.GetComponent<IButtonActivation>();

            if (activateViaButton != null)
            {
                activateViaButton.Activate(ActivationDuration);
            }
        }


    }

    private IEnumerator LitButtonEnumerator(float howLong)
    {
        meshRenderer.material = litMaterial;

        yield return new WaitForSeconds(howLong);

        meshRenderer.material = unLitMaterial;
    }

   


}
