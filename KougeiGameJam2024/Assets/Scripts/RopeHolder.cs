using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeHolder : MonoBehaviour
{
    [SerializeField] private Rigidbody holdPoint;
    [SerializeField] private PlayerMovement playerMovement;

    private bool isReleased = false;

    private void Start()
    {
        playerMovement.OnReleased += _ =>
        {
            holdPoint.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            isReleased = true;
        };
    }

    private void Update()
    {
        if (!isReleased)
        {
            holdPoint.transform.position = transform.position;
        }
    }
}