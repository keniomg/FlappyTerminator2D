using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private int _damage;
    private Collider _ownCollider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _ownCollider = GetComponent<Collider>();
    }

    internal void Initialize(int damage, Collider collider)
    {
        _damage = damage;
        Physics.IgnoreCollision(collider, _ownCollider);
    }

    public void Shoot(Vector3 startPoint, Vector3 speed)
    {
        _rigidbody.position = startPoint;
        _rigidbody.velocity = speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider != null)
        {
            Health health = collision.collider.GetComponentInParent<Health>();

            if (health != null)
            {
                health.TakeDamage(_damage);
            }
        }
    }
}