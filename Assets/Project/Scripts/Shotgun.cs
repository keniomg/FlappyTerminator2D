using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private int _damage;
    [SerializeField] private float _maxDistance;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private Projectile _prefab;
    [SerializeField] private float _projectileVelocity;

    private PlayerInputController _playerInputController;
    private Collider _playerCollider;

    private void OnDestroy()
    {
        _playerInputController.Shooted -= OnShoot;
    }

    public void Initialize(PlayerInputController playerInputController, Collider collider)
    {
        _playerInputController = playerInputController;
        _playerInputController.Shooted += OnShoot;
        _playerCollider = collider;
    }

    private void OnShoot()
    {
        ProjectileShoot(_startPoint.position, GetDirection() * _projectileVelocity);
    }

    private void ProjectileShoot(Vector3 startPoint, Vector3 direction)
    {
        Projectile projectile = Instantiate(_prefab);
        projectile.Initialize(_damage, _playerCollider);
        projectile.Shoot(startPoint, direction);
    }

    private Vector3 GetDirection()
    {
        if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit hitInfo))
        {
            Debug.Log(hitInfo.transform.position);
            return (hitInfo.point - _startPoint.position).normalized;
        }

        return _cameraTransform.forward;
    }
}