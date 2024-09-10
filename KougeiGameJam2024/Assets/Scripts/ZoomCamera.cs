using System;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField] private float min;
    [SerializeField] private float max;
    [SerializeField] private Rope targetRope;

    private void Start()
    {
        min = transform.localPosition.z;
    }

    private void LateUpdate()
    {
        // カメラを近づける
        Vector3 position = targetRope.transform.position;
        transform.position = new Vector3(position.x, position.y, min + max * targetRope.Length);
    }
}