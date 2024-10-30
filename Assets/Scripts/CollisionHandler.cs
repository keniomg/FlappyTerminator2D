using System;
using UnityEngine;

public class CollisionHandler<Initializer, Border> : BaseCollisionHandler where Initializer : Unit where Border : UnitBorder
{
    [SerializeField] private GeneralCollisionEventInvoker _collisionEventInvoker;

    public event Action<GameObject, GameObject> Collided;

    private void OnDisable()
    {
        _collisionEventInvoker.Unregister(gameObject.GetInstanceID(), Collided);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Border border) ||
            collision.gameObject.TryGetComponent(out Initializer initializer) ||
            (collision.gameObject.TryGetComponent(out Projectile projectile) && projectile.Owner != Owner))
        {
            _collisionEventInvoker.InvokeEvent(gameObject.GetInstanceID(), gameObject, collision.gameObject);
        }
    }

    public void Initialize(LayerMask owner)
    {
        Owner = owner;
        _collisionEventInvoker.Register(gameObject.GetInstanceID(), Collided);
    }
}