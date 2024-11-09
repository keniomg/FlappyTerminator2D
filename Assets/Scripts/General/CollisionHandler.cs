using System;
using UnityEngine;

public class CollisionHandler<UnitType, Border> : BaseCollisionHandler where UnitType : Unit where Border : UnitBorder
{
    [SerializeField] private GeneralCollisionEventInvoker _collisionEventInvoker;

    private SoundEventsInvoker _soundEventsInvoker;

    public event Action<GameObject, GameObject> Collided;

    private void OnDisable()
    {
        _collisionEventInvoker.Unregister(gameObject.GetInstanceID(), Collided);
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

    public void Initialize(LayerMask owner, SoundEventsInvoker soundEventsInvoker)
    {
        Owner = owner;
        _collisionEventInvoker.Register(gameObject.GetInstanceID(), Collided);
        _soundEventsInvoker = soundEventsInvoker;
    }
}