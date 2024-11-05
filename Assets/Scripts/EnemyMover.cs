using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector2 _direction;

    public void Initialize(Vector2 direction)
    {
        _direction = direction;
    }

    public void Move()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }
}