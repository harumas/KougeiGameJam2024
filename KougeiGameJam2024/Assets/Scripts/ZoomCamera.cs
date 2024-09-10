using System;
using Cinemachine;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float paddingOffset;
    [SerializeField] private CinemachineVirtualCamera mainCamera;
    [SerializeField] private CinemachineVirtualCamera lPlayerCamera;
    [SerializeField] private CinemachineVirtualCamera rPlayerCamera;
    [SerializeField] private PlayerMovement lPlayerMovement;
    [SerializeField] private PlayerMovement rPlayerMovement;

    private void Start()
    {
        lPlayerMovement.OnReleased += SwitchCameraPriority;
        rPlayerMovement.OnReleased += SwitchCameraPriority;
    }

    private void SwitchCameraPriority(bool isRight)
    {
        mainCamera.Priority = 0;

        if (isRight)
        {
            lPlayerCamera.Priority = 0;
            rPlayerCamera.Priority = 1;
        }
        else
        {
            lPlayerCamera.Priority = 1;
            rPlayerCamera.Priority = 0;
        }
    }

    private void LateUpdate()
    {
        UpdateMainCamera();
    }

    private void UpdateMainCamera()
    {
        // カメラを近づける
        Vector3 diff = pointB.position - pointA.position;
        mainCamera.transform.position = pointA.position + diff * 0.5f + new Vector3(0f, 0f, -10f);
        mainCamera.m_Lens.OrthographicSize = diff.magnitude * 0.5f - diff.magnitude * diff.magnitude * paddingOffset;
    }
}