using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private ScoreEventInvoker _scoreEventInvoker;
    [SerializeField] private ScenesEventsInvoker _scenesEventsInvoker;

    public int DefaultScore { get; private set; }
    public int Score { get; private set; }

    public event Action ScoreChanged;

    private void Awake()
    {
        DefaultScore = 0;
        Score = DefaultScore;
    }

    private void OnEnable()
    {
        _scoreEventInvoker.ValueChanged += OnScoreChanged;
        _scenesEventsInvoker.SceneStatusChanged += OnSceneStatusChanged;
    }

    private void OnDisable()
    {
        _scoreEventInvoker.ValueChanged -= OnScoreChanged;
        _scenesEventsInvoker.SceneStatusChanged -= OnSceneStatusChanged;
    }

    private void OnScoreChanged(int value)
    {
        Score += value;
        ScoreChanged?.Invoke();
    }

    private void OnSceneStatusChanged(ScenesEventsTypes sceneEventType)
    {
        if (sceneEventType is ScenesEventsTypes.GameRestarted ||
            sceneEventType is ScenesEventsTypes.GameStarted ||
            sceneEventType is ScenesEventsTypes.OpenedMainMenu)
        {
            Score = DefaultScore;
        }
    }
}