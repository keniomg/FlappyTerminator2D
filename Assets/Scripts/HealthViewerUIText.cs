using TMPro;
using UnityEngine;

public class HealthViewerUIText : HealthViewerUI
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    protected override void OnValueChanged(GameObject healthObject, UnitStatusTypes statusType)
    {
        _textMeshPro.text = $"Health: {Health.CurrentValue}";
    }
}