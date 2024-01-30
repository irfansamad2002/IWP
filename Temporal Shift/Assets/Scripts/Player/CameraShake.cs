using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraShake : MonoBehaviour
{
    [SerializeField] CinemachineImpulseSource cinemachineImpulseSource;

 
    private void OnEnable()
    {
        BootUpPortal.onPortalOpenEvent += BootUpPortal_onPortalOpenEvent;
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

    private void OnDisable()
    {
        BootUpPortal.onPortalOpenEvent -= BootUpPortal_onPortalOpenEvent;

    }
}
