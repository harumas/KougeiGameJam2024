using System;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float paddingOffset;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        // カメラを近づける
        Vector3 diff = pointB.position - pointA.position;
        transform.position = pointA.position + diff * 0.5f + new Vector3(0f, 0f, -10f);
        mainCamera.orthographicSize = diff.magnitude * 0.5f - diff.magnitude * diff.magnitude * paddingOffset;
    }
}