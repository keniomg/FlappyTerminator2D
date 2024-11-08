using UnityEngine;

public abstract class PlayerHealthViewerUI : MonoBehaviour
{
    protected PlayerHealth Health;

    private void OnDisable()
    {
        Health.ValueChanged -= OnValueChanged;
    }

    public virtual void Initialize(PlayerHealth playerHealth)
    {
        Health = playerHealth;
        Health.ValueChanged += OnValueChanged;
    }

    protected abstract void OnValueChanged(int value, int maximumValue);
}