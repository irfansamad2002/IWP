using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPipeWhenSecondPhase : MonoBehaviour
{
    private void OnEnable()
    {
        BossParent.OnPhaseTwoEvent += BossParent_OnPhaseTwoEvent;
    }

   
    private void OnDisable()
    {
        BossParent.OnPhaseTwoEvent -= BossParent_OnPhaseTwoEvent;

    }

    private void BossParent_OnPhaseTwoEvent()
    {
        Destroy(gameObject);
    }


}
