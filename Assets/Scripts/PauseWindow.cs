using UnityEngine;

public class PauseWindow : Window
{
    [SerializeField] private Window _settings;
    [SerializeField] private ScenesEventsInvoker _scenesEventInvoker;

    public override void OnCloseWindowButtonClicked()
    {
        base.OnCloseWindowButtonClicked();
        _scenesEventInvoker.Invoke(ScenesEventsTypes.GameContinued);
    }

    public override void OnOpenWindowButtonClicked()
    {
        base.OnOpenWindowButtonClicked();
        _scenesEventInvoker.Invoke(ScenesEventsTypes.GamePaused);
    }

    public void OnRestartButtonClicked()
    {
        _scenesEventInvoker.Invoke(ScenesEventsTypes.GameRestarted);
        SoundInvoker.Invoke(SoundTypes.ButtonClicked);
    }

    public void OnSettingsButtonClicked()
    {
        _settings.OnOpenWindowButtonClicked();
    }

    public void OnMainMenuButtonClicked()
    {
        base.OnCloseWindowButtonClicked();
        _scenesEventInvoker.Invoke(ScenesEventsTypes.OpenedMainMenu);
    }

    public void OnQuitButtonClicked()
    {
        SoundInvoker.Invoke(SoundTypes.ButtonClicked);
        Application.Quit();
    }
}