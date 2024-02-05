using DigitalRuby.LightningBolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class LaserAimByRay : MonoBehaviour
{
    [SerializeField] float DebugLineLength = 20f;
    [SerializeField] LayerMask ignoreColliders;
    
    LightningBoltScript daDefaultScript;
    AudioSource audioSource;

    private Vector3 hitPosition;
    private bool hitPlayer;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        daDefaultScript = GetComponent<LightningBoltScript>();
        BootUpPortal.onPortalOpenEvent += BootUpPortal_onPortalOpenEvent;
        audioSource.Play();
    }

   
    private void OnDisable()
    {
        BootUpPortal.onPortalOpenEvent -= BootUpPortal_onPortalOpenEvent;
    }

    private void BootUpPortal_onPortalOpenEvent()
    {
        daDefaultScript.enabled = false;
        this.enabled = false;
    }


    private void Update()
    {
        //if(Physics.cast)
        
        if (Physics.Raycast(transform.position,transform.forward, out RaycastHit hit, ignoreColliders))
        {
            //Debug.Log(hit.collider.name);
           
            ChargingEmmision hitGenerator = hit.collider.gameObject.GetComponent<ChargingEmmision>();
           if (hitGenerator != null)
           {
                hitGenerator.Charge();
           }

            IHitable iHitable = hit.collider.gameObject.GetComponent<IHitable>();

            if (iHitable != null && !hitPlayer)
            {

                hitPlayer = true;
                StartCoroutine(playerGetHitRoutine(iHitable));
            }


           

            hitPosition = hit.point;
            daDefaultScript.StartPosition = transform.position;
            daDefaultScript.EndPosition = hitPosition;

        }
        else
        {
            hitPosition = hit.point;
            daDefaultScript.StartPosition = transform.position;
            daDefaultScript.EndPosition = transform.position + (transform.forward * DebugLineLength);

        }
    }

    IEnumerator playerGetHitRoutine(IHitable iHitable)
    {
        iHitable.Hit();
        yield return new WaitForSeconds(1f);
        hitPlayer = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Debug.Log(firePoint);
        Gizmos.DrawRay(transform.position, transform.forward* DebugLineLength);
    }

}
