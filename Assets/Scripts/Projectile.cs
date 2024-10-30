using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(ProjectileCollisionHandler))]

public class Projectile : MonoBehaviour, IMovable
{
    private float _speed;
    private ProjectileCollisionHandler _handler;
    private Vector2 _direction;

    public LayerMask Owner {get; private set; }
    public int Damage {get; private set; }

    private void Awake()
    {
        _handler = GetComponent<ProjectileCollisionHandler>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Initialize(Vector2 direction, GeneralCollisionEventInvoker invoker, AttackerData attackerData)
    {
        Damage = attackerData.ProjectileDamage;
        _speed = attackerData.ProjectileSpeed;
        Owner = attackerData.AttackOwner;
        _direction = direction;
        _handler.Initialize(Owner, invoker);
    }

    public void Move()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }
}