using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRewindDraw : MonoBehaviour
{
    LineRenderer lineRenderer;
    [SerializeField] TimeBody timeBody;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lineRenderer.positionCount = timeBody.pointsInTime.Count;

    }

    public void CalculateLine()
    {
        for (int i = 0; i < timeBody.pointsInTime.Count; i++)
        {
            Debug.Log(timeBody.pointsInTime.Count);
            lineRenderer.SetPosition(i, timeBody.pointsInTime[i].position);

        }
    }

    public void ShowLine()
    {
        CalculateLine();
        lineRenderer.enabled = true;
    }

    public void HideLine()
    {
        lineRenderer.enabled = false;
    }
}
