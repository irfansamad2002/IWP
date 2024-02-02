using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePillarGoUp : MonoBehaviour
{
    public float howHighPillarGo;
    //public Vector3 howHighPillarGo;

    float lerpDuration = 5;
    Vector3 initialPosition;

    private void OnEnable()
    {
        BossParent.OnPhaseTwoEvent += BossParent_OnPhaseTwoEvent;
    }

   

    private void OnDisable()
    {
        BossParent.OnPhaseTwoEvent -= BossParent_OnPhaseTwoEvent;
    }

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void BossParent_OnPhaseTwoEvent()
    {
        RisePillar();
    }

    [ContextMenu("RISE")]
    public void RisePillar()
    {
        StartCoroutine(Lerp());
    }

    IEnumerator Lerp()
    {
        Vector3 targetPosition = new Vector3(initialPosition.x, initialPosition.y + howHighPillarGo, initialPosition.z);
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
