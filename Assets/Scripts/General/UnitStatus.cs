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

    public void ResetStatus()
    {
        IsDied = false;
        IsDamaged = false;
        IsAttack = false;
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
                HandleDieStatusEvent();
                break;
            case UnitStatusTypes.Attack:
                StartCoroutine(HandleAttackStatusEvent());
                break;
            case UnitStatusTypes.Damaged:
                StartCoroutine(HandleDamagedStatusEvent());
                break;
            default:
                break;
        }
    }

    private void HandleDieStatusEvent()
    {
        IsDied = true;
    }

    private IEnumerator HandleAttackStatusEvent()
    {
        IsAttack = true;

        yield return null;

        IsAttack = false;
    }

    private IEnumerator HandleDamagedStatusEvent()
    {
        IsDamaged = true;

        yield return null;

        IsDamaged = false;
    }
}