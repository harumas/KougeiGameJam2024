using System;
using Cinemachine;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float offsetZ;
    [SerializeField] private float paddingOffset;
    [SerializeField] private float cameraDamping;
    [SerializeField] private CinemachineBrain cameraBrain;
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

    public void SwitchCameraPriority(bool isRight)
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

    public void SetEaseDuration(float easeDuration)
    {
        cameraBrain.m_DefaultBlend.m_Time = easeDuration;
    }

    private void LateUpdate()
    {
        UpdateMainCamera();
    }

    private void UpdateMainCamera()
    {
        // カメラを近づける
        Vector3 diff = pointB.position - pointA.position;
        Vector3 xy = pointA.position + diff * 0.5f + new Vector3(0f, 0f, -10f);
        float z = offsetZ + -diff.magnitude * 0.5f + diff.magnitude * diff.magnitude * paddingOffset;
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, new Vector3(xy.x, xy.y, z), cameraDamping * Time.deltaTime);
    }
}