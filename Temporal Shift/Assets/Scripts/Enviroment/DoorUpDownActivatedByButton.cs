using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUpDownActivatedByButton : MonoBehaviour, IButtonActivation
{
    Animator _anim;

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
    }

    IEnumerator OpenDoorForHowLong(float howLong)
    {
        _anim.SetBool("character_nearby", true);
        yield return new WaitForSeconds(howLong);
        _anim.SetBool("character_nearby", false);
    }
}
