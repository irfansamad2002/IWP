using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastWall : MonoBehaviour
{
    public float HowMuchFaster = 4f;
    private void OnTriggerEnter(Collider other)
    {
        ISpeedTimeable iSpeedTimeable  = other.GetComponent<ISpeedTimeable>();
        if (iSpeedTimeable != null)
        {
            iSpeedTimeable.MoveFastSpeed(HowMuchFaster);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");

    }

}
