using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopObjectAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void StopAnimating()
    {
        animator.enabled = false;
    }

    public void StartAnimating()
    {
        animator.enabled = true;


    }

}
