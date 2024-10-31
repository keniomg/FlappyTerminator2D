using UnityEngine;
using UnityEngine.UIElements;

public class HealthViewerUIHealthBar : HealthViewerUI
{
    [SerializeField] private Slider _healthbar;

    private float _defaultValue;

    private void OnEnable()
    {
        _healthbar.value = _defaultValue;
    }

    protected override void OnValueChanged(GameObject healthObject, UnitStatusTypes statusType)
    {
        _healthbar.value = (float)Health.CurrentValue / Health.MaximumValue;
    }
}