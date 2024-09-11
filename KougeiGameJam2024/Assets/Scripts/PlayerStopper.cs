using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStopper : MonoBehaviour
{
    [SerializeField] private PlayerMovement lPlayerMovement;
    [SerializeField] private PlayerMovement rPlayerMovement;

    private void Start()
    {
        lPlayerMovement.OnReleased += _ =>
        {
            rPlayerMovement.End();
        };
        
        rPlayerMovement.OnReleased += _ =>
        {
            lPlayerMovement.End();
        };
    }
}