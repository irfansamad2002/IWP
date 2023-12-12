using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAim : MonoBehaviour
{
    public Transform PlayerTransform;
    public float AgroTreshold = .8f;
    [SerializeField] TurretAttackBasedOnTime turretAtk;
    [SerializeField] float rotationSpeed = 4f;
    public Transform firePoint; // The point where the projectile will be spawned

   
    private void Update()
    {
        Vector3 toPlayer = PlayerTransform.position - transform.position;

        // Use dot product to determine the angle between turret forward and player direction
        float dot = Vector3.Dot(transform.forward, toPlayer.normalized);

        if (dot < AgroTreshold)
            return;
        if (toPlayer.magnitude <= turretAtk.maxDistance)
        {
            Debug.Log("in range");

            // Calculate the rotation to look at the player
            Quaternion targetRotation = Quaternion.LookRotation(toPlayer);

            // Smoothly rotate the turret head towards the player
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        if (PlayerTransform == null)
        {
            return;
        }
        Vector3 toPlayer = PlayerTransform.position - transform.position;

        float dot = Vector3.Dot(transform.forward, toPlayer.normalized);


        if (dot >= AgroTreshold && toPlayer.magnitude <= turretAtk.maxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(firePoint.position, toPlayer);
        }
        else
        {
            Gizmos.color = Color.red;
            //Debug.Log(firePoint);
            Gizmos.DrawRay(firePoint.position, firePoint.forward * turretAtk.maxDistance);
        }


    }
}
