using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWall : MonoBehaviour
{
    [SerializeField] GameObject atkWallPrefab;
    [SerializeField] List<Transform> spawnPositions = new List<Transform>();

    [ContextMenu("Atk Start")]
    public void AtkStart()
    {
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            GameObject go = Instantiate(atkWallPrefab, spawnPositions[i].transform);
            //go.GetComponent<ForceFieldHurtPlayer>().Attack();

            Utils.RunAfterDelay(this, 1f, go.GetComponent<ForceFieldHurtPlayer>().Attack);
        }
        
    }
}
