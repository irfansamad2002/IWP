using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraShake : MonoBehaviour
{
    [SerializeField] CinemachineImpulseSource cinemachineImpulseSource;
    bool isMakingScreenShake;
    private void OnEnable()
    {
        BootUpPortal.onPortalOpenEvent += BootUpPortal_onPortalOpenEvent;
        BossParent.OnPhaseTwoEvent += BossParent_OnPhaseTwoEvent;
    }

   
    private void OnDisable()
    {
        BootUpPortal.onPortalOpenEvent -= BootUpPortal_onPortalOpenEvent;
        BossParent.OnPhaseTwoEvent -= BossParent_OnPhaseTwoEvent;
    }

    private void BossParent_OnPhaseTwoEvent()
    {
        ShakeScreenForHowLong(5f);
    }


    [ContextMenu("SHAKE")]
    private void BootUpPortal_onPortalOpenEvent()
    {
        StartCoroutine(shakeScreen());

    }

    IEnumerator shakeScreen()
    {
        
        cinemachineImpulseSource.GenerateImpulseWithForce(.1f);
        yield return new WaitForSeconds(.1f);

        cinemachineImpulseSource.GenerateImpulseWithForce(.2f);

        yield return new WaitForSeconds(.1f);

        cinemachineImpulseSource.GenerateImpulseWithForce(.6f);

        yield return new WaitForSeconds(.1f);

        cinemachineImpulseSource.GenerateImpulseWithForce(.8f);

        yield return new WaitForSeconds(.2f);

        cinemachineImpulseSource.GenerateImpulseWithForce(1.2f);

        yield return new WaitForSeconds(.3f);

        cinemachineImpulseSource.GenerateImpulseWithForce(1.5f);

        yield return new WaitForSeconds(.4f);

        cinemachineImpulseSource.GenerateImpulseWithForce(1.7f);

        yield return new WaitForSeconds(.5f);
    }


    public void ShakeScreenForHowLong(float duration)
    {
        StartCoroutine(AllowScreenShakeRoutine(duration));
    }

    IEnumerator AllowScreenShakeRoutine(float duration)
    {
        isMakingScreenShake = true;
        yield return new WaitForSeconds(duration);
        isMakingScreenShake = false;
    }

    private void Update()
    {
        if(isMakingScreenShake)
            cinemachineImpulseSource.GenerateImpulseWithForce(UnityEngine.Random.Range(.1f, 1.0f));

    }
}
