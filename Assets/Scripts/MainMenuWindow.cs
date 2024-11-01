using UnityEngine;

public class MainMenuWindow : Window
{
    [SerializeField] private Window _settings;
    [SerializeField] private ScenesEventsInvoker _scenesEventInvoker;

    public void OnPlayButtonClicked()
    {
        base.OnCloseWindowButtonClicked();
        _scenesEventInvoker.Invoke(ScenesEventsTypes.GameStarted);
    }

    public void OnSettingsButtonClicked()
    {
        _settings.OnOpenWindowButtonClicked();
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
