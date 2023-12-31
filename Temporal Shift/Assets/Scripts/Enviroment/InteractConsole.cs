using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractConsole : MonoBehaviour, IInteractable
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] float shootForce = 500f;

    public void Interacted()
    {
        SpawnTheBall();
    }
    [ContextMenu("SpawnTheBall")]
    void SpawnTheBall()
    {
        GameObject go = Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation);

        Rigidbody rb = go.GetComponent<Rigidbody>();

        go.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shootForce);
    }
    

   
}
