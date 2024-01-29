using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSight : MonoBehaviour
{
    [SerializeField] Transform firePoint; // The point where the projectile will be spawned
    [SerializeField] float maxDistance = 30f;
    [SerializeField] LayerMask layerMask;

    private Vector3 hitPosition;
    LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.positionCount = 2;

    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(firePoint.transform.position, firePoint.forward, out RaycastHit hit, maxDistance, ~layerMask))
        {
            hitPosition = hit.point;
            lineRenderer.SetPosition(0, firePoint.transform.position);
            lineRenderer.SetPosition(1, hitPosition);
        }
    }
}
