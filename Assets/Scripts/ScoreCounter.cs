using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private ScoreEventInvoker _scoreEventInvoker;

    public int Score { get; private set; }

    public event Action ScoreChanged;

    private void OnEnable()
    {
        _scoreEventInvoker.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _scoreEventInvoker.ScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int value)
    {
        Score += value;
        ScoreChanged?.Invoke();
    }
}