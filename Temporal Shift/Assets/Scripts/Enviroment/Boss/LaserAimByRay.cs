using DigitalRuby.LightningBolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAimByRay : LightningBoltScript
{

    //LineRenderer lineRenderer;
    private Vector3 hitPosition;

    [SerializeField] float DebugLineLength = 20f;

    private void OnEnable()
    {
        //lineRenderer = GetComponent<LineRenderer>();
        //lineRenderer.positionCount = 2;
        
    }

    private void Update()
    {
        int layerMask = ~LayerMask.GetMask("PressERange");  // Exclude the layer of your button
        if (Physics.Raycast(transform.position,transform.forward, out RaycastHit hit, ~layerMask))
        {
            hitPosition = hit.point;
            //lineRenderer.SetPosition(0, transform.position);
            //lineRenderer.SetPosition(1, hitPosition);
            base.StartPosition = transform.position;
            base.EndPosition = hitPosition;
        }
        else
        {
            hitPosition = hit.point;
            //lineRenderer.SetPosition(0, transform.position);
            //lineRenderer.SetPosition(1, transform.position + (transform.forward * DebugLineLength));
            base.StartPosition = transform.position;
            base.EndPosition = transform.position + (transform.forward * DebugLineLength);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Debug.Log(firePoint);
        Gizmos.DrawRay(transform.position, transform.forward* DebugLineLength);
    }

}
