using UnityEngine;

public abstract class Health<UnitAttacker, UnitCollisionHandler> : BaseHealth, IDamagable 
    where UnitAttacker : AttackerData 
    where UnitCollisionHandler : BaseCollisionHandler
{
    [SerializeField] private GeneralCollisionEventInvoker _invoker;

    private void OnEnable()
    {
        CurrentValue = MaximumValue;
        _invoker.Register(gameObject.GetInstanceID(), HandleDamage);
    }

    protected override void OnDisable()
    {
        _invoker.Unregister(gameObject.GetInstanceID(), HandleDamage);
        base.OnDisable();
    }

    public virtual void TakeDamage(int decreaseValue, LayerMask attacker)
    {
        if (Own != attacker)
        {
            CurrentValue = Mathf.Clamp(CurrentValue - decreaseValue, MinimumValue, MaximumValue);

            if (CurrentValue != 0)
            {
                UnitStatusEventInvoker.Invoke(gameObject.GetInstanceID(), gameObject, UnitStatusTypes.Damage);
            }
            else
            {
                UnitStatusEventInvoker.Invoke(gameObject.GetInstanceID(), gameObject, UnitStatusTypes.Die);
            }

            InvokeValueChangedEvent();
        }
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