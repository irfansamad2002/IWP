using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static void RunAfterDelay(MonoBehaviour monoBehaviour, float delay, Action task)
    {
        monoBehaviour.StartCoroutine(RunAfterDelayRoutine(delay, task));
    }

    public static void RunAfterSecondsRealtime(MonoBehaviour monoBehaviour, float delay, Action task)
    {
        monoBehaviour.StartCoroutine(RunAfterSecondsRealtime(delay, task));
    }

    private static IEnumerator RunAfterDelayRoutine(float delay, Action task)
    {
        yield return new WaitForSeconds(delay);
        task.Invoke();
    }

    private static IEnumerator RunAfterSecondsRealtime(float delay, Action task)
    {
        yield return new WaitForSecondsRealtime(delay);
        task.Invoke();
    }

    public static Vector3 ClampVector3(Vector3 vector, Vector3 min, Vector3 max)
    {
        float x = Mathf.Clamp(vector.x, min.x, max.x);
        float y = Mathf.Clamp(vector.y, min.y, max.y);
        float z = Mathf.Clamp(vector.z, min.z, max.z);

        return new Vector3(x, y, z);
    }
}
