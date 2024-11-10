using UnityEngine;
using UnityEngine.UI;

public class PauseWindow : Window
{
    [SerializeField] private Window _settings;
    [SerializeField] private ScenesEventsInvoker _scenesEventsInvoker;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _quitButton;

    private void Awake()
    {
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        _quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
        _settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
        _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        _quitButton.onClick.RemoveListener(OnQuitButtonClicked);
    }

    public override void OnCloseWindowButtonClicked()
    {
        base.OnCloseWindowButtonClicked();
        _scenesEventsInvoker.Invoke(ScenesEventsTypes.GameContinued);
    }

    public override void OnOpenWindowButtonClicked()
    {
        base.OnOpenWindowButtonClicked();
        _scenesEventsInvoker.Invoke(ScenesEventsTypes.OpenedPauseMenu);
    }

    public void OnRestartButtonClicked()
    {
        _scenesEventsInvoker.Invoke(ScenesEventsTypes.GameRestarted);
        SoundInvoker.Invoke(SoundTypes.ButtonClicked);
    }

    public void OnSettingsButtonClicked()
    {
        _settings.OnOpenWindowButtonClicked();
        SoundInvoker.Invoke(SoundTypes.ButtonClicked);
    }

    public void OnMainMenuButtonClicked()
    {
        base.OnCloseWindowButtonClicked();
        _scenesEventsInvoker.Invoke(ScenesEventsTypes.OpenedMainMenu);
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
        SoundInvoker.Invoke(SoundTypes.ButtonClicked);
    }
}