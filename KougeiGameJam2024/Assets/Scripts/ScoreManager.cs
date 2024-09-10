using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Rope rope;
    [SerializeField] private PlayerMovement lPlayerMovement;
    [SerializeField] private PlayerMovement rPlayerMovement;
    [SerializeField] private float minScore;
    [SerializeField] private float maxScore;

    public event Action<bool, float> OnScored;

    private void Start()
    {
        lPlayerMovement.OnReleased += CalculateScore;
        rPlayerMovement.OnReleased += CalculateScore;
    }

    private void CalculateScore(bool isRight)
    {
        Debug.Log($"Score: {minScore + maxScore * rope.Length}");
        OnScored?.Invoke(isRight, minScore + maxScore * rope.Length);
    }
}