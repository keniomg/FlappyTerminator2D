using UnityEngine;

public class PlayerStatus : UnitStatus
{
    protected override void OnUnitStatusChanged(GameObject changedObject, UnitStatusTypes statusType)
    {
        switch (statusType)
        {
            case UnitStatusTypes.Die:
                HandleDieStatusEvent();
                break;
            case UnitStatusTypes.Attack:
                HandleTriggerStatusEvent(IsAttacked);
                break;
            case UnitStatusTypes.Jump:
                HandleTriggerStatusEvent(IsJumped);
                break;
            case UnitStatusTypes.Damage:
                HandleTriggerStatusEvent(IsDamaged);
                break;
            default:
                break;
        }
    }
}