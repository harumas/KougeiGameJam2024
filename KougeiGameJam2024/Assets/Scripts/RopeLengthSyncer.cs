using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class RopeLengthSyncer : MonoBehaviour
    {
        [SerializeField] private Transform pointA;
        [SerializeField] private Transform pointB;
        [SerializeField] private Rope rope;

        private void Update()
        {
            rope.Length = Mathf.InverseLerp(0f, rope.GetMaxLength(), Vector3.Distance(pointA.position, pointB.position));
        }
    }
}