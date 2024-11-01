using TMPro;
using UnityEngine;

public class PlayerHealthViewerUIText : PlayerHealthViewerUI
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    protected override void OnValueChanged(int value, int maximumValue)
    {
        _textMeshPro.text = $"Health: {value}/{maximumValue}";
    }
}