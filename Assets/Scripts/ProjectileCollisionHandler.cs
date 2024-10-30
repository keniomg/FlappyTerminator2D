using System;
using UnityEngine;

public class ProjectileCollisionHandler : MonoBehaviour
{
    private GeneralCollisionEventInvoker _invoker;
    
    public LayerMask Owner {get; private set; }

    public event Action<GameObject, GameObject> Collided;

    private void OnDisable()
    {
        _invoker.Unregister(gameObject.GetInstanceID(), Collided);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.TryGetComponent(out AttackerData attackerData) && attackerData.AttackOwner != Owner) ||
            collision.gameObject.TryGetComponent(out Projectile projectile) && projectile.Owner != Owner ||
            collision.gameObject.TryGetComponent(out PlayerBorder border))
        {
            _invoker.InvokeEvent(gameObject.GetInstanceID(), gameObject, collision.gameObject);
        }
    }

    public void Initialize(LayerMask owner, GeneralCollisionEventInvoker invoker)
    {
        Owner = owner;
        _invoker = invoker;
        _invoker.Register(gameObject.GetInstanceID(), Collided);
    }
}