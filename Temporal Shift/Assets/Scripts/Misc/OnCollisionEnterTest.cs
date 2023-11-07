using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionEnterTest : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log(collision);

    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log(collision);

    }
}
