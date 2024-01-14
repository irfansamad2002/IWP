using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawColliderBox : MonoBehaviour
{
    [SerializeField] BoxCollider boxCollider;


    private void OnDrawGizmosSelected()
    {
        if (boxCollider != null)
        {
            Gizmos.color = new Color(0, 1, 0, 0.1f);
            Gizmos.DrawCube(boxCollider.bounds.center, boxCollider.size);
        }
    }
   
}
