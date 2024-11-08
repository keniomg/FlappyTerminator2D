using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthViewerUIHealthBar : PlayerHealthViewerUI
{
    [SerializeField] private Slider _healthbar;

    private float _defaultValue;

    private void Awake()
    {
        _defaultValue = 1;
    }

    private void OnEnable()
    {
        _healthbar.value = _defaultValue;
    }

    protected override void OnValueChanged(int value, int maximumValue)
    {
        _healthbar.value = (float)value / maximumValue;
    }
}