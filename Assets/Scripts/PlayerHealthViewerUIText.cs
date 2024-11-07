using TMPro;
using UnityEngine;

public class PlayerHealthViewerUIText : PlayerHealthViewerUI
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    public override void Initialize(PlayerHealth playerHealth)
    {
        base.Initialize(playerHealth);
        _textMeshPro.text = $"Health: {Health.CurrentValue}/{Health.MaximumValue}";
    }

    protected override void OnValueChanged(int value, int maximumValue)
    {
        _textMeshPro.text = $"Health: {value}/{maximumValue}";
    }
}