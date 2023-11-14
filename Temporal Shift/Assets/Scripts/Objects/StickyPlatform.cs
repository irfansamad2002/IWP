using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    LoopWaypointsObjectMovement platformMovement;

    private void Awake()
    {
        platformMovement = GetComponent<LoopWaypointsObjectMovement>();
    }

    



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Movement>().externalMotion = platformMovement.DirectionToWaypoint * platformMovement.speed;
           
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Movement>().externalMotion = platformMovement.DirectionToWaypoint * platformMovement.speed;

        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Movement>().externalMotion = Vector3.zero;

            //Debug.Log("OnTriggerExit");




        }
    }

  
}
