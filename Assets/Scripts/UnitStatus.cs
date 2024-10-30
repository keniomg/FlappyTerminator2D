using UnityEngine;

public class UnitStatus : MonoBehaviour
{
    private UnitStatusEventInvoker _statusInvoker;

    public bool IsDied { get; private set; }
    public bool IsJumped { get; private set; }
    public bool IsAttacked { get; private set; }
    public bool IsDamaged { get; private set; }

    private void OnEnable()
    {
        ResetStatus();
    }

    private void OnDisable()
    {
        _statusInvoker.Unregister(gameObject.GetInstanceID(), OnUnitStatusChanged);
    }

    public void Initialize(UnitStatusEventInvoker statusInvoker)
    {
        _statusInvoker = statusInvoker;
        _statusInvoker.Register(gameObject.GetInstanceID(), OnUnitStatusChanged);
    }

    protected virtual void OnUnitStatusChanged(GameObject changedObject, UnitStatusTypes statusType)
    {
        switch (statusType)
        {
            case UnitStatusTypes.Die:
                HandleDieStatusEvent();
                break;
            case UnitStatusTypes.Attack:
                HandleTriggerStatusEvent(IsAttacked);
                break;
            case UnitStatusTypes.Damage:
                HandleTriggerStatusEvent(IsDamaged);
                break;
            default:
                break;
        }
    }

    protected virtual void HandleDieStatusEvent()
    {
        IsDied = true;
    }

    protected virtual void HandleTriggerStatusEvent(bool trigger)
    {
        trigger = true;
        trigger = false;
    }

    private void ResetStatus()
    {
        IsDied = false;
        IsDamaged = false;
        IsAttacked = false;
        IsDamaged = false;
    }
}