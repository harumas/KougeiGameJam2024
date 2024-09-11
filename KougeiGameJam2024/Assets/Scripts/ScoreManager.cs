using System;
using DefaultNamespace;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Rope rope;
    [SerializeField] private ShieldJudge shieldJudge;
    [SerializeField] private float minScore;
    [SerializeField] private float maxScore;

    public event Action<bool, float> OnScored;

    private void Start()
    {
        shieldJudge.Damaged += CalculateScore;
    }

    private void CalculateScore(bool isRight, bool isShield)
    {
        Debug.Log($"Score: {minScore + maxScore * rope.Length}");

        if (isRight)
        {
            if (isShield)
            {
                Invoke(nameof(AddRightScore), 0.2f);
            }
            else
            {
                AddLeftScore();
            }
        }
        else
        {
            if (isShield)
            {
                Invoke(nameof(AddLeftScore), 0.2f);
            }
            else
            {
                AddRightScore();
            }
        }
    }

    private void AddRightScore()
    {
        OnScored?.Invoke(true, minScore + maxScore * rope.Length);
    }

    private void AddLeftScore()
    {
        OnScored?.Invoke(false, minScore + maxScore * rope.Length);
    }
}