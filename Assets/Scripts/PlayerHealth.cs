using UnityEngine;

public class PlayerHealth : Health<AttackerData, EnemyCollisionHandler> 
{

    private ScenesEventsInvoker _scenesEventInvoker;

    public void ResetValue()
    {
        CurrentValue = MaximumValue;
        InvokeValueChangedEvent();
    }

    public void Initialize(UnitStatusEventInvoker unitStatusEventInvoker, ScenesEventsInvoker scenesEventsInvoker)
    {
        base.Initialize(unitStatusEventInvoker);
        _scenesEventInvoker = scenesEventsInvoker;
    }

    public override void TakeDamage(int decreaseValue, LayerMask attacker)
    {
        base.TakeDamage(decreaseValue, attacker);

        if (CurrentValue == 0)
        {
            _scenesEventInvoker.Invoke(ScenesEventsTypes.PlayerDied);
        }
    }
}