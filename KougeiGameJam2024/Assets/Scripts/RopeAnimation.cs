using System;
using UnityEngine;

public class RopeAnimation : MonoBehaviour
{
    [SerializeField] private Rope rope;
    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    [SerializeField] private Transform bodyTransform;

    private Vector3 initialScale;

    private void Start()
    {
        initialScale = bodyTransform.localScale;
    }

    private void Update()
    {
        SetRopeLength(rope.Length);
    }

    private void SetRopeLength(float length)
    {
        meshRenderer.SetBlendShapeWeight(0, length * 100);
        bodyTransform.localScale = initialScale + Vector3.right * (rope.GetMaxLength() * length);
    }
}
