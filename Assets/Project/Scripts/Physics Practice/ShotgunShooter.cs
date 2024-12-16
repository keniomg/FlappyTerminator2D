using System;
using UnityEngine;

public class ShotgunShooter : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private int _damage;
    [SerializeField] private float _maxDistance;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private Projectile _prefab;
    [SerializeField] private float _projectileVelocity;
    [SerializeField] private float _impactForce = 10f;
    [SerializeField] private Transform _decal;
    [SerializeField] private float _decalOffset;

    private PlayerInputController _playerInputController;
    private Collider _playerCollider;
    private bool _isShootAvailable;

    public event Action Shooted;
    public event Action ReloadStarted;

    public void Initialize(PlayerInputController playerInputController, Collider collider)
    {
        _playerInputController = playerInputController;
        _playerInputController.Shooted += OnShoot;
        _playerCollider = collider;
        _isShootAvailable = true;
    }

    private void OnDestroy()
    {
        _playerInputController.Shooted -= OnShoot;
    }

    public void InvokeReloadStarted()
    {
        ReloadStarted?.Invoke();
    }

    public void FinishReload()
    {
        _isShootAvailable = true;
    }

    private void OnShoot()
    {
        //ProjectileShoot(_startPoint.position, GetDirection() * _projectileVelocity);
        if (_isShootAvailable)
        {
            RaycastShoot(_cameraTransform.position, _cameraTransform.forward);
        }
    }

    private void ProjectileShoot(Vector3 startPoint, Vector3 direction)
    {
        Projectile projectile = Instantiate(_prefab);
        projectile.Initialize(_damage, _playerCollider);
        projectile.Shoot(startPoint, direction);
    }

    private void RaycastShoot(Vector3 startPoint, Vector3 direcion)
    {
        Shooted?.Invoke();
        _isShootAvailable = false;

        float sphereRadius = 0.1f;

        if (Physics.SphereCast(startPoint, sphereRadius, direcion, out RaycastHit hitInfo, Mathf.Infinity, _targetLayer, QueryTriggerInteraction.Ignore))
        {
            Transform decal = Instantiate(_decal, hitInfo.transform);
            decal.position = hitInfo.point + hitInfo.normal * _decalOffset;
            decal.LookAt(hitInfo.point);
            float rotationAngle = 180;
            decal.Rotate(Vector3.right, rotationAngle, Space.Self);

            Health health = hitInfo.collider.GetComponentInParent<Health>();

            if (health != null)
            {
                health.TakeDamage(_damage);
            }

            Rigidbody targetRigidbody = hitInfo.rigidbody;

            if (targetRigidbody != null)
            {
                targetRigidbody.AddForceAtPosition(direcion * _impactForce, hitInfo.point);
            }
        }
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