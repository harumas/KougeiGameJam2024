using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HpManager : MonoBehaviour
{
    [SerializeField] private float maxHp;
    [SerializeField] private float increaseHpValue;
    [SerializeField] private float increaseInterval;
    [SerializeField] private float damageMultiplier;
    [SerializeField] private Slider lPlayerSlider;
    [SerializeField] private Slider rPlayerSlider;
    [SerializeField] private PlayerMovement lPlayerMovement;
    [SerializeField] private PlayerMovement rPlayerMovement;
    [SerializeField] private Rope rope;

    [Header("プレイヤーのHp")] [SerializeField] private float lPlayerHp;
    [SerializeField] private float rPlayerHp;

    private bool isPlaying;

    private void Start()
    {
        lPlayerHp = maxHp;
        rPlayerHp = maxHp;

        lPlayerMovement.OnReleased += isRight => { AddHp(!isRight, rope.Length * -damageMultiplier); };

        rPlayerMovement.OnReleased += isRight => { AddHp(!isRight, rope.Length * -damageMultiplier); };
    }

    private IEnumerator IncreaseHpCoroutine()
    {
        while (isPlaying)
        {
            AddHp(true, increaseHpValue);
            AddHp(false, increaseHpValue);

            yield return new WaitForSeconds(increaseInterval);
        }
    }

    public void AddHp(bool isRight, float delta)
    {
        if (isRight)
        {
            rPlayerHp += delta;
            rPlayerHp = Mathf.Clamp(rPlayerHp, 0, maxHp);
            rPlayerSlider.value = rPlayerHp / maxHp;
        }
        else
        {
            lPlayerHp += delta;
            lPlayerHp = Mathf.Clamp(lPlayerHp, 0, maxHp);
            lPlayerSlider.value = lPlayerHp / maxHp;
        }
    }

    public void Begin()
    {
        isPlaying = true;
        StartCoroutine(nameof(IncreaseHpCoroutine));
    }

    public void End()
    {
        isPlaying = false;
    }
}