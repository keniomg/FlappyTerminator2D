using System;
using UnityEngine;

public class CollisionHandler<UnitType, Border> : BaseCollisionHandler where UnitType : Unit where Border : UnitBorder
{
    [SerializeField] private GeneralCollisionEventInvoker _collisionEventInvoker;

    private SoundEventsInvoker _soundEventsInvoker;
    private UnitStatusEventInvoker _unitStatusEventInvoker;

    public event Action<GameObject, GameObject> Collided;

    private void OnEnable()
    {
        HandleRaised();
    }

    private void OnDisable()
    {
        _collisionEventInvoker.Unregister(gameObject.GetInstanceID(), Collided);
        _unitStatusEventInvoker.Unregister(gameObject.GetInstanceID(), OnUnitStatusChanged);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out UnitType unitType))
        {
            _soundEventsInvoker.Invoke(SoundTypes.UnitsCollided);
            _collisionEventInvoker.InvokeEvent(gameObject.GetInstanceID(), gameObject, collision.gameObject);
        }
        else if (collision.gameObject.TryGetComponent(out Border border) || collision.gameObject.TryGetComponent(out Projectile projectile) && projectile.Owner != Owner)
        {
            _collisionEventInvoker.InvokeEvent(gameObject.GetInstanceID(), gameObject, collision.gameObject);
        }
    }

    public void Initialize(LayerMask owner, SoundEventsInvoker soundEventsInvoker, UnitStatusEventInvoker unitStatusEventInvoker)
    {
        Owner = owner;
        _collisionEventInvoker.Register(gameObject.GetInstanceID(), Collided);
        _soundEventsInvoker = soundEventsInvoker;
        _unitStatusEventInvoker = unitStatusEventInvoker;
        _unitStatusEventInvoker.Register(gameObject.GetInstanceID(), OnUnitStatusChanged);
    }

    private void OnUnitStatusChanged(GameObject changedObject, UnitStatusTypes unitStatusType)
    {
        if (unitStatusType is UnitStatusTypes.Died)
        {
            HandleDied();
        }
    }

    private void HandleDied()
    {
        if (gameObject.TryGetComponent(out Collider2D collder))
        {
            collder.enabled = false;
        }
    }

    private void HandleRaised()
    {
        if (gameObject.TryGetComponent(out Collider2D collder))
        {
            collder.enabled = true;
        }
    }
}