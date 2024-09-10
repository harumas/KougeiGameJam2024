using System;
using UnityEngine;

public class RopeRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int divideCount;

    private void Update()
    {
        lineRenderer.positionCount = divideCount;

        Transform head = transform.GetChild(0);
        Transform tail = transform.GetChild(transform.childCount - 1);
        Vector3 diff = (tail.position - head.position) / (divideCount - 1);

        for (var i = 0; i < divideCount; i++)
        {
            lineRenderer.SetPosition(i, head.position + diff * i);
        }
    }
}