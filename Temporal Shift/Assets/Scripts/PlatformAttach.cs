using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAttach : MonoBehaviour
{
    public GameObject Player;
    public BoxCollider boxCollider;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision Enter");

        if (collision.gameObject == Player)
        {
            Player.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Collisione exit");

        if (collision.gameObject == Player)
        {
                
            Player.transform.parent = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");

        if (other.gameObject == Player)
        {
            Player.transform.parent = transform;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");


        if (other.gameObject == Player)
        {

            Player.transform.parent = null;
        }
    }



}
