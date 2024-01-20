using DigitalRuby.LightningBolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAimByRay : MonoBehaviour
{
    [SerializeField] float DebugLineLength = 20f;
    LightningBoltScript daDefaultScript;

    private Vector3 hitPosition;



    private void OnEnable()
    {
        daDefaultScript = GetComponent<LightningBoltScript>();
    }

    private void Update()
    {
        int layerMask = ~LayerMask.GetMask("PressERange");  // Exclude the layer of your button
        if (Physics.Raycast(transform.position,transform.forward, out RaycastHit hit, ~layerMask))
        {
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Debug.Log(firePoint);
        Gizmos.DrawRay(transform.position, transform.forward* DebugLineLength);
    }

}
