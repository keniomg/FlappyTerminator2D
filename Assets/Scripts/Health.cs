using System;
using UnityEngine;

public abstract class Health<UnitAttacker, UnitCollisionHandler> : BaseHealth, IDamagable 
    where UnitAttacker : AttackerData 
    where UnitCollisionHandler : BaseCollisionHandler
{
    [SerializeField] private GeneralCollisionEventInvoker _invoker;

    [field: SerializeField] public int MaximumValue { get; private set; }

    private int _minimumValue;
    private UnitStatusEventInvoker _unitStatusEventInvoker;

    public int CurrentValue { get; private set; }

    public event Action<GameObject, UnitStatusTypes> Over;

    private void OnEnable()
    {
        CurrentValue = MaximumValue;
        _invoker.Register(gameObject.GetInstanceID(), HandleDamage);
    }

    private void OnDisable()
    {
        _invoker.Unregister(gameObject.GetInstanceID(), HandleDamage);
        _unitStatusEventInvoker.Unregister(gameObject.GetInstanceID(), Over);
    }

    public void TakeDamage(int decreaseValue, LayerMask attacker)
    {
        if (Own != attacker)
        {
            CurrentValue = Mathf.Clamp(CurrentValue - decreaseValue, _minimumValue, MaximumValue);

            if (CurrentValue != 0)
            {
                _unitStatusEventInvoker.Invoke(gameObject.GetInstanceID(), gameObject, UnitStatusTypes.Damage);
            }
            else
            {
                _unitStatusEventInvoker.Invoke(gameObject.GetInstanceID(), gameObject, UnitStatusTypes.Die);
            }
        }
    }

    public void Initialize(UnitStatusEventInvoker unitStatusEventInvoker)
    {
        _unitStatusEventInvoker = unitStatusEventInvoker;
        _unitStatusEventInvoker.Register(gameObject.GetInstanceID(), Over);
    }

    protected void HandleDamage(GameObject ownGameObject, GameObject attackerGameObject)
    {
        if (attackerGameObject.TryGetComponent(out Projectile projectile))
        {
            TakeDamage(projectile.Damage, projectile.Owner);
        }
        else if (attackerGameObject.TryGetComponent(out UnitAttacker attacker)
            && attackerGameObject.TryGetComponent(out UnitCollisionHandler collisionHandler))
        {
            TakeDamage(attacker.CollisionDamage, collisionHandler.Owner);
        }
    }
}