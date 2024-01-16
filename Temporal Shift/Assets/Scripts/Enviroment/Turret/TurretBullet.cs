using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour, IStopTimeable
{
    public GameObject explosionVFX;
    public float bulletSpeed = 1f;

    Rigidbody rb;
    float originalSpeed;

    bool stoppedTime;

    private void Start()
    {
        originalSpeed = bulletSpeed;
        stoppedTime = false;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Vector3 moveDirection = transform.forward * bulletSpeed;

        rb.AddForce(moveDirection, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (stoppedTime)
            return;
        // = hit.collider.gameObject.GetComponent<IHitable>();
        IHitable iHitable = collision.gameObject.GetComponent<IHitable>();

        if (iHitable != null)
        {
            iHitable.Hit();
        }

        //hit anythin else
        GameObject newExplosionFX = Instantiate(explosionVFX, transform.position, Quaternion.identity);
        Destroy(newExplosionFX, 2);
        Destroy(gameObject);
    }

    public void StopMoving()
    {
        stoppedTime = true;
        bulletSpeed = 0f;
        rb.isKinematic = true;
        
    }

    public void StartMoving()
    {
        stoppedTime = false;
        bulletSpeed = originalSpeed;
        rb.isKinematic = false;

    }
}
