using UnityEngine;

public class SceneEventsUIContainer : MonoBehaviour
{
    [SerializeField] private ScenesEventsInvoker _eventsInvoker;
    [SerializeField] private GameObject[] _disablingOnGameInterrupted;
    [SerializeField] private GameObject[] _enablingOnGameInterrupted;

    private void OnEnable()
    {
        _eventsInvoker.SceneStatusChanged += OnGameOver;
    }

    private void OnDisable()
    {
        _eventsInvoker.SceneStatusChanged -= OnGameOver;
    }

    private void OnGameOver(ScenesEventsTypes sceneEventType)
    {
        switch (sceneEventType)
        {
            case ScenesEventsTypes.PlayerDied:
            case ScenesEventsTypes.PlayerTouchedPlatform:
                DisableElements(_disablingOnGameInterrupted); 
                EnableElements(_enablingOnGameInterrupted);
                break;
            case ScenesEventsTypes.GameRestarted:
            case ScenesEventsTypes.GameStarted:
                DisableElements(_enablingOnGameInterrupted);
                EnableElements(_disablingOnGameInterrupted);
                break;
        }
    }

    private void EnableElements(GameObject[] gameObjects)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(true);
        }
    }

    private void DisableElements(GameObject[] gameObjects) 
    {
        foreach(GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(false); 
        }
    }
}