using System.Collections;
using UnityEngine;

public class UnitStatus : MonoBehaviour
{
    private UnitStatusEventInvoker _statusInvoker;

    public bool IsDied { get; private set; }
    public bool IsAttack { get; private set; }
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

    private void OnUnitStatusChanged(GameObject changedObject, UnitStatusTypes statusType)
    {
        switch (statusType)
        {
            case UnitStatusTypes.Died:
                IsDied = true;
                break;
            case UnitStatusTypes.Attack:
                IsAttack = true;
                break;
            case UnitStatusTypes.Damaged:
                IsDamaged = true;
                break;
            default:
                break;
        }
    }

    private void HandleTriggerBoolStatusEvent(ref bool triggerBool)
    {
        triggerBool = true;
        StartCoroutine(ResetTriggerNextFrame(triggerBool));
    }

    private IEnumerator ResetTriggerNextFrame(bool triggerBool)
    {
        yield return null;
        triggerBool = false;
    }

    private void ResetStatus()
    {
        IsDied = false;
        IsDamaged = false;
        IsAttack = false;
        IsDamaged = false;
    }
}