using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("操作関係")]
    [SerializeField]private float MoveSpeed;
    [Header("動きの制限関係")]
    [Tooltip("制限を始める距離")]
    [SerializeField]private float LimitStartDistance;
    [SerializeField]private float LimitEndByStartDistance;
    [SerializeField]private AnimationCurve LimitCurve;
    [SerializeField]private bool IsRightMove;
    private float MovedDistance;
    // Update is called once per frame
    void Update()
    {
        if(IsRightMove && Input.GetKey(KeyCode.RightArrow) && LimitStartDistance >= transform.position.x)
        {
            Vector3 CurrentPosition = transform.position;
            CurrentPosition.x += MoveSpeed * Time.deltaTime;
            transform.position = CurrentPosition;
        }else if(IsRightMove && Input.GetKey(KeyCode.RightArrow))
        {
            PositionLimit();
        }

        if(!IsRightMove && Input.GetKey(KeyCode.A) && LimitStartDistance >= -transform.position.x)
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
            CurrentPosition.x += MoveSpeed * LimitCurve.Evaluate((transform.position.x - LimitStartDistance)/LimitEndByStartDistance) * Time.deltaTime;
            transform.position = CurrentPosition;
        }

        if(!IsRightMove && Input.GetKey(KeyCode.A))
        {
            Vector3 CurrentPosition = transform.position;
            CurrentPosition.x += -MoveSpeed * LimitCurve.Evaluate((-transform.position.x - LimitStartDistance)/LimitEndByStartDistance) * Time.deltaTime;
            Debug.Log("MoveValue" + (-transform.position.x - LimitStartDistance)/LimitEndByStartDistance);
            transform.position = CurrentPosition;
        }
    }
}
