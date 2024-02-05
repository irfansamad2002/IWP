using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAfterMultiButtonActivated : MonoBehaviour
{
    [SerializeField] ActivateByMultipleButtonsEvent buttonsEvent;
    [SerializeField] GameObject FX;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        buttonsEvent.OnActivation += ButtonsEvent_OnActivation;
    }

    private void ButtonsEvent_OnActivation()
    {
        Instantiate(FX, gameObject.transform);
        audioSource.Play();
    }

    private void OnDisable()
    {
        buttonsEvent.OnActivation -= ButtonsEvent_OnActivation;

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
