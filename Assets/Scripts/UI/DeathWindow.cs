using UnityEngine;
using UnityEngine.UI;

public class DeathWindow : Window
{
    [SerializeField] private ScenesEventsInvoker _scenesEventsInvoker;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _quitButton;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
        _quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
        _quitButton.onClick.RemoveListener(OnQuitButtonClicked);
    }

    public void OnRestartButtonClicked()
    {
        OnCloseWindowButtonClicked();
        _scenesEventsInvoker.Invoke(ScenesEventsTypes.GameRestarted);
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
        SoundInvoker.Invoke(SoundTypes.ButtonClicked);
    }
}