using UnityEngine;
using UnityEngine.UI;

public class OpenButton : MonoBehaviour
{
    [SerializeField] private Window _targetWindow;
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        _targetWindow.OnOpenWindowButtonClicked();
    }
}
