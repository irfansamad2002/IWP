using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTurret : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletAtkPos;
    [SerializeField] float shootInterval = 3f; // Time interval between shots



    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Shoot), 0f, shootInterval);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawRay(transform.position, transform.position +  bulletAtkPos.position);

        Gizmos.DrawRay(bulletAtkPos.position, bulletAtkPos.forward);
    }
    [ContextMenu("Shoot")]
    private void Shoot()
    {
        Instantiate(bulletPrefab, bulletAtkPos.position, bulletAtkPos.rotation);

    }

}
