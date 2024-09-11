using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("敵のオブジェクト")]
    [Tooltip("アタッチ")]
    [SerializeField]
    private GameObject EnemyObj;

    [Header("操作関係")] [SerializeField] private float MoveSpeed;

    [Tooltip("アタッチ")] [SerializeField] private Rope ropeScript;
    [SerializeField] private bool IsRightMove;
    [SerializeField] private bool IsHoldingRubber;

    [Header("シールド関係")]
    [Tooltip("「経過時間＊ヒール値」で計算されます")]
    [Range(0, 2f)]
    [SerializeField]
    private float ShieldDurationHeal;

    [SerializeField] private float ShieldDuration;
    [SerializeField] private float ShieldCoolDown;
    [SerializeField] private Shield shield;
    private bool IsShieldBroken;
    private float InitialShieldDuration;
    public bool IsUsingShield, isWalking_R, isWalking_L;


    public event Action<bool> OnReleased;
    private bool IsReleased;

    void Start()
    {
        InitialShieldDuration = ShieldDuration;
    }

    private bool BeginGame = false;

    public void Begin()
    {
        BeginGame = true;
    }

    public void End()
    {
        BeginGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (BeginGame)
        {
            Pull();

            RubberHold();

            Shield();
        }
    }

    void Shield()
    {
        if (IsRightMove)
        {
            if (Input.GetKey(KeyCode.DownArrow) && !IsShieldBroken)
            {
                IsUsingShield = true;

                ShieldDuration -= Time.deltaTime;
                if (ShieldDuration < 0)
                {
                    StartCoroutine(ShieldBreak());
                }
            }
            else
            {
                IsUsingShield = false;

                if (!IsShieldBroken && ShieldDuration <= InitialShieldDuration)
                    ShieldDuration += Time.deltaTime * ShieldDurationHeal;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.S) && !IsShieldBroken)
            {
                IsUsingShield = true;

                ShieldDuration -= Time.deltaTime;
                if (ShieldDuration < 0)
                {
                    StartCoroutine(ShieldBreak());
                }
            }
            else
            {
                IsUsingShield = false;

                if (!IsShieldBroken && ShieldDuration <= InitialShieldDuration)
                    ShieldDuration += Time.deltaTime * ShieldDurationHeal;
            }
        }

        if (ShieldDuration < 0.8f)
        {
            shield.Notice();
        }
        else
        {
            shield.StopNotice();
        }
    }

    IEnumerator ShieldBreak()
    {
        IsShieldBroken = true;
        yield return new WaitForSeconds(ShieldCoolDown);

        IsShieldBroken = false;
    }

    void RubberHold()
    {
        if (IsRightMove)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                IsHoldingRubber = true;
            }
            else if (!IsShieldBroken)
            {
                OnReleased?.Invoke(true);
                End();
                IsHoldingRubber = false;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.D))
            {
                IsHoldingRubber = true;
            }
            else if (!IsShieldBroken)
            {
                OnReleased?.Invoke(false);
                End();
                IsHoldingRubber = false;
            }
        }
    }

    void Pull()
    {
        if (IsRightMove && Input.GetKey(KeyCode.RightArrow) && !IsShieldBroken)
        {
            Vector3 CurrentPosition = transform.position;
            CurrentPosition.x += MoveSpeed * ropeScript.GetRopeDecayRate() * Time.deltaTime;
            transform.position = CurrentPosition;
            isWalking_R = true;
        }
        else
        {
            isWalking_R = false;
        }

        if (!IsRightMove && Input.GetKey(KeyCode.A) && !IsShieldBroken)
        {
            Vector3 CurrentPosition = transform.position;
            CurrentPosition.x += -MoveSpeed * ropeScript.GetRopeDecayRate() * Time.deltaTime;
            transform.position = CurrentPosition;
            isWalking_L = true;
        }
        else
        {
            isWalking_L = false;
        }
    }
}