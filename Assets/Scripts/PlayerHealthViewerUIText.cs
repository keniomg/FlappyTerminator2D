using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerHealthViewerUIText : PlayerHealthViewerUI
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private void Awake()
    {
        _textMeshPro.text = $"Health: {Health.CurrentValue}/{Health.MaximumValue}";
    }

    protected override void OnValueChanged(int value, int maximumValue)
    {
        _textMeshPro.text = $"Health: {value}/{maximumValue}";
    }
}