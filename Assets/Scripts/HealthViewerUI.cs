using UnityEngine;

public abstract class HealthViewerUI<HealthType, UnitType, BorderType> : MonoBehaviour where HealthType : Health<AttackerData, CollisionHandler<UnitType, BorderType>> 
    where UnitType : Unit
    where BorderType : UnitBorder
{
    protected HealthType Health;

    private void OnDisable()
    {
        Health.ValueChanged -= OnValueChanged;
    }

    public void Initialize(HealthType health)
    {
        Health = health;
        Health.ValueChanged += OnValueChanged;
    }

    protected abstract void OnValueChanged(GameObject healthObject, UnitStatusTypes statusType);
}