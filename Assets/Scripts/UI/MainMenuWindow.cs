using UnityEngine;
using UnityEngine.UI;

public class MainMenuWindow : Window
{
    [SerializeField] private ScenesEventsInvoker _scenesEventInvoker;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnPlayButtonClicked);
        _quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlayButtonClicked);
        _quitButton.onClick.RemoveListener(OnQuitButtonClicked);
    }

    public void OnPlayButtonClicked()
    {
        base.OnCloseWindowButtonClicked();
        _scenesEventInvoker.Invoke(ScenesEventsTypes.GameStarted);
    }

    public void OnQuitButtonClicked()
    {
        SoundInvoker.Invoke(SoundTypes.ButtonClicked);
        Application.Quit();
    }
}