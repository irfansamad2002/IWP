using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnEnterObjectToTargetObject : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider collision)
    {
        InitialPositionVector3 initialPositionVector3;
        if (collision.gameObject.TryGetComponent<InitialPositionVector3>(out initialPositionVector3))
        {
            GameObject currentObject = collision.gameObject;

            currentObject.transform.position = initialPositionVector3.InitialPosition;

            currentObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            
        }
    }


}
