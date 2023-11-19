using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretAudio
{
    public AudioClip shotClip;
}

[System.Serializable]
public class TurretFX
{

    [Tooltip("Muzzle transform position")]
    public Transform muzzle;
    [Tooltip("Spawn this GameObject when shooting")]
    public GameObject shotFX;
    [Tooltip("Spawn this GameObject when on hit position")]
    public GameObject explosionFX;
}

public class TurretAttackBasedOnTime : MonoBehaviour
{
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public Transform firePoint; // The point where the projectile will be spawned
    public float shootInterval = 3f; // Time interval between shots
    public float maxDistance = 30f;
    public TurretAudio SFX;
    public TurretFX VFX;


    private Vector3 hitPosition;
    LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        
        InvokeRepeating(nameof(Shoot), 0f, shootInterval);
    }
    [ContextMenu("Shoot")]
    void Shoot()
    {
        //play sound
        GetComponent<AudioSource>().PlayOneShot(SFX.shotClip, Random.Range(0.3f, 0.5f));
        //play animation
        GetComponent<Animator>().SetTrigger("Shot");
        //spawn shotvfx on muzzle
        GameObject newShotFX = Instantiate(VFX.shotFX, VFX.muzzle);
        Destroy(newShotFX, 2);

        if (Physics.Raycast(firePoint.transform.position, firePoint.forward, out RaycastHit hit, maxDistance))
        {
            hitPosition = hit.point;
            GameObject newExplosionFX = Instantiate(VFX.explosionFX, hitPosition, Quaternion.identity);
            Destroy(newExplosionFX, 2);
        }


    }

    private void Update()
    {
        if (Physics.Raycast(firePoint.transform.position, firePoint.forward, out RaycastHit hit, maxDistance))
        {
            hitPosition = hit.point;
            lineRenderer.SetPosition(0, firePoint.transform.position);
            lineRenderer.SetPosition(1, hitPosition);
        }
    }




}
