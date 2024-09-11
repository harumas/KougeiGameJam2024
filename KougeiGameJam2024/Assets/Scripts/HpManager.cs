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
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private Image lPlayerSlider;
    [SerializeField] private Image rPlayerSlider;
    [SerializeField] private Transform lPlayerTransform;
    [SerializeField] private Transform rPlayerTransform;

    private bool isPlaying;
    public event Action Damaged;

    private void Start()
    {
        if (GameStateData.Instance.IsFirstRound)
        {
            GameStateData.Instance.Initialize(maxHp);
        }

        scoreManager.OnScored += (isRight, score) =>
        {
            float distance = Vector3.Distance(lPlayerTransform.position, rPlayerTransform.position);
            AddHp(isRight, distance * -damageMultiplier);
            Damaged?.Invoke();
        };
        
        SetHp(true, GameStateData.Instance.RPlayerHp);
        SetHp(false, GameStateData.Instance.LPlayerHp);
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

    private void AddHp(bool isRight, float delta)
    {
        if (isRight)
        {
            GameStateData.Instance.RPlayerHp += delta;
            rPlayerSlider.fillAmount = GameStateData.Instance.RPlayerHp / maxHp;
        }
        else
        {
            GameStateData.Instance.LPlayerHp += delta;
            lPlayerSlider.fillAmount = GameStateData.Instance.LPlayerHp / maxHp;
        }
    }

    private void SetHp(bool isRight, float value)
    {
        if (isRight)
        {
            GameStateData.Instance.RPlayerHp = value;
            rPlayerSlider.fillAmount = GameStateData.Instance.RPlayerHp / maxHp;
        }
        else
        {
            GameStateData.Instance.LPlayerHp = value;
            lPlayerSlider.fillAmount = GameStateData.Instance.LPlayerHp / maxHp;
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