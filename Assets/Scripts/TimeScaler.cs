using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    [SerializeField] private ScenesEventsInvoker _scenesEventInvoker;

    private void OnEnable()
    {
        _scenesEventInvoker.SceneStatusChanged += OnSceneStatusChanged;
    }

    private void OnDisable()
    {
        _scenesEventInvoker.SceneStatusChanged -= OnSceneStatusChanged;
    }

    private void OnSceneStatusChanged(ScenesEventsTypes systemEventsType)
    {
        if (systemEventsType is ScenesEventsTypes.GamePaused || 
            systemEventsType is ScenesEventsTypes.PlayerDied)
        {
            Pause();
        }
        else if (systemEventsType is ScenesEventsTypes.GameContinued || 
            systemEventsType is ScenesEventsTypes.GameRestarted ||
            systemEventsType is ScenesEventsTypes.GameStarted)
        {
            Play();
        }
    }

    private void Pause()
    {
        Time.timeScale = 0.0f;
    }

    private void Play()
    {
        Time.timeScale = 1.0f;
    }
}