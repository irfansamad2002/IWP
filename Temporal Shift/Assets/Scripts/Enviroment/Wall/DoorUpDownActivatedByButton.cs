using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUpDownActivatedByButton : MonoBehaviour, IButtonActivation
{
    [SerializeField] AudioClip doorOpen, doorClose;

    Animator _anim;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _anim = GetComponent<Animator>();
    }


    public void Activate()
    {

    }

    public void Activate(float howLong)
    {
        StartCoroutine(OpenDoorForHowLong(howLong));
        audioSource.PlayOneShot(doorOpen);
    }

    IEnumerator OpenDoorForHowLong(float howLong)
    {
        _anim.SetBool("character_nearby", true);
        yield return new WaitForSeconds(howLong);
        _anim.SetBool("character_nearby", false);
        audioSource.PlayOneShot(doorClose);

    }
}
