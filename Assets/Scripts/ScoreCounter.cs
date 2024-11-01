using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private ScoreEventInvoker _scoreEventInvoker;
    [SerializeField] private ScenesEventsInvoker _scenesEventsInvoker;

    private int _defaultScore;

    public int Score { get; private set; }

    public event Action ScoreChanged;

    private void Awake()
    {
        _defaultScore = 0;
        Score = _defaultScore;
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
            Score = _defaultScore;
        }
    }
}