using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDoorActivatedByButton : MonoBehaviour, IButtonActivation
{


    Animator _anim;
    private void OnEnable()
    {
        _anim = GetComponent<Animator>();
    }
    public void Activate()
    {
        //Debug.Log("Activate this door");
    }

    public void Activate(float howLong)
    {
        StartCoroutine(OpenDoorForSomeTime(howLong));
    }

    IEnumerator OpenDoorForSomeTime(float someTime)
    {
        _anim.Play("WallOpen");
        yield return new WaitForSeconds(someTime);
        _anim.Play("WallClose");

    }


}
