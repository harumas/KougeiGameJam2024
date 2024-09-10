using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("操作関係")]
    [SerializeField]private GameObject EnemyObj;
    [Header("操作関係")]
    [SerializeField]private float MoveSpeed;
    [Header("動きの制限関係")]
    [Tooltip("制限を始める距離")]
    [Range(0f, 1f)]
    [SerializeField]private float LimitStartLength;
    [SerializeField]private AnimationCurve LimitCurve;
    [SerializeField]private bool IsRightMove;
    [SerializeField]private float CurrentRopeLength;
    private float MaxRopeLength;
    // Update is called once per frame
    void Update()
    {

        CurrentRopeLength = Vector3.Distance(transform.position,EnemyObj.transform.position);

        if(IsRightMove && Input.GetKey(KeyCode.RightArrow) && LimitStartLength >= CurrentRopeLength)
        {
            Vector3 CurrentPosition = transform.position;
            CurrentPosition.x += MoveSpeed * Time.deltaTime;
            transform.position = CurrentPosition;
        }else if(IsRightMove && Input.GetKey(KeyCode.RightArrow))
        {
            PositionLimit();
        }

        if(!IsRightMove && Input.GetKey(KeyCode.A) && LimitStartLength >= CurrentRopeLength)
        {
            Vector3 CurrentPosition = transform.position;
            CurrentPosition.x += -MoveSpeed * Time.deltaTime;
            transform.position = CurrentPosition;
        }else if(!IsRightMove && Input.GetKey(KeyCode.A))
        {
            PositionLimit();
        }

        
    }

    void PositionLimit()
    {
        if(IsRightMove && Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 CurrentPosition = transform.position;
            CurrentPosition.x += MoveSpeed * LimitCurve.Evaluate(CurrentRopeLength/MaxRopeLength) * Time.deltaTime;
            transform.position = CurrentPosition;
        }

        if(!IsRightMove && Input.GetKey(KeyCode.A))
        {
            Vector3 CurrentPosition = transform.position;
            CurrentPosition.x += -MoveSpeed * LimitCurve.Evaluate(CurrentRopeLength/MaxRopeLength) * Time.deltaTime;
            transform.position = CurrentPosition;
        }
    }
}
