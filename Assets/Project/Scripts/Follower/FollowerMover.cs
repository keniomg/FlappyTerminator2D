using UnityEngine;

public class FollowerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 6;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _followOffset = 999;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_rigidbody == null || _targetTransform == null)
        {
            return;
        }

        Vector3 directionToTarget = _targetTransform.position - _transform.position;

        if (directionToTarget.sqrMagnitude > _followOffset * _followOffset)
        {
            Vector3 moveDirection = new Vector3(directionToTarget.x, 0, directionToTarget.z).normalized;
            Vector3 nextPosition = _transform.position + moveDirection * _moveSpeed * Time.fixedDeltaTime;

            _rigidbody.MovePosition(nextPosition);
        }
    }
}