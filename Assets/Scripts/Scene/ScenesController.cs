using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private ScenesEventsInvoker _scenesEventInvoker;

    private void OnEnable()
    {
        _scenesEventInvoker.SceneStatusChanged += OnSceneStatusChanged;
    }

    private void OnDisable()
    {
        _scenesEventInvoker.SceneStatusChanged -= OnSceneStatusChanged;
    }

    private void OnSceneStatusChanged(ScenesEventsTypes sceneEventType)
    {
        if (sceneEventType is ScenesEventsTypes.GameRestarted ||
            sceneEventType is ScenesEventsTypes.GameStarted)
        {
            SceneManager.LoadScene("Level");
        }
        else if (sceneEventType is ScenesEventsTypes.OpenedMainMenu)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}