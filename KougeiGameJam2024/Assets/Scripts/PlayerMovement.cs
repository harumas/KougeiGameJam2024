using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("敵のオブジェクト")]
    [SerializeField]private GameObject EnemyObj;
    [Header("操作関係")]
    [SerializeField]private float MoveSpeed;

    [SerializeField]private Rope ropeScript;
    [SerializeField]private bool IsRightMove;
    [SerializeField]private bool IsHoldingRubber;

    public event Action<bool> OnReleased;



    // Update is called once per frame
    void Update()
    {
        ropeScript.Length = Vector3.Distance(EnemyObj.transform.position,transform.position) / ropeScript.GetMaxLength();

        Pull();

        RubberHold();

        Shield();
    }

    void Shield()
    {

    }

    void RubberHold()
    {
        if(IsRightMove && Input.GetKey(KeyCode.LeftArrow))
        {
            IsHoldingRubber = true;
        }else{
            OnReleased?.Invoke(true);
            IsHoldingRubber = false;
        }

        if(!IsRightMove && Input.GetKey(KeyCode.D))
        {
            IsHoldingRubber = true;
        }else{
            OnReleased?.Invoke(false);
            IsHoldingRubber = false;
        }
    }

    void Pull()
    {
        if(IsRightMove && Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 CurrentPosition = transform.position;
            CurrentPosition.x += MoveSpeed * ropeScript.GetRopeDecayRate() * Time.deltaTime;
            transform.position = CurrentPosition;
        }

        if(!IsRightMove && Input.GetKey(KeyCode.A))
        {
            Vector3 CurrentPosition = transform.position;
            CurrentPosition.x += -MoveSpeed * ropeScript.GetRopeDecayRate() * Time.deltaTime;
            transform.position = CurrentPosition;
        }
    }
}
