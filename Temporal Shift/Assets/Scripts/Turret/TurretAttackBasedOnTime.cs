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
    public GameObject muzzleVFX;
}

[SelectionBase]
public class TurretAttackBasedOnTime : MonoBehaviour, IStopTimeable
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
        GameObject newShotFX = Instantiate(VFX.muzzleVFX, VFX.muzzle);
        Destroy(newShotFX, 2);

        Instantiate(projectilePrefab, VFX.muzzle.position, VFX.muzzle.rotation);

        


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

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawRay(firePoint.transform.position, firePoint.forward * maxDistance);

    //}

    public void StopMoving()
    {
        Debug.Log("stop turret");
        CancelInvoke();
    }

    public void StartMoving()
    {
        Debug.Log("StartMoving turret");
        InvokeRepeating(nameof(Shoot), 0f, shootInterval);

    }
}
