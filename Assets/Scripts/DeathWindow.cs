using UnityEngine;

public class DeathWindow : Window
{
    [SerializeField] private ScenesEventsInvoker _scenesEventsInvoker;

    public void OnRestartButtonClicked()
    {
        base.OnCloseWindowButtonClicked();
        _scenesEventsInvoker.Invoke(ScenesEventsTypes.GameRestarted);
    }

    public void OnMainMenuButtonClicked()
    {
        base.OnCloseWindowButtonClicked();
        _scenesEventsInvoker.Invoke(ScenesEventsTypes.OpenedMainMenu);
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}