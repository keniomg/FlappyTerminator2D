using System.Collections;
using System.Collections.Generic;
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
        //RaycastShoot(_cameraTransform.position, _cameraTransform.forward, _maxDistance, _targetLayer);
        ProjectileShoot(_startPoint.position, _startPoint.forward * _projectileVelocity);
    }

    private void ProjectileShoot(Vector3 startPoint, Vector3 direction)
    {
        Projectile projectile = Instantiate(_prefab);
        projectile.Initialize(_damage, _playerCollider);
        projectile.Shoot(startPoint, GetDirection());
    }

    private Vector3 GetDirection()
    {
        if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit hitInfo))
        {
            Debug.Log(hitInfo.transform.position);
            return (hitInfo.transform.position - _startPoint.position).normalized;
        }

        return Vector3.forward;
    }

    private void RaycastShoot(Vector3 startPoint, Vector3 direction, float maxDistance, LayerMask targetLayer)
    {
        if (Physics.Raycast(startPoint, direction, out RaycastHit hitInfo, maxDistance, targetLayer, QueryTriggerInteraction.Ignore))
        {
            Health health = hitInfo.collider.GetComponentInParent<Health>();

            if (health != null)
            {
                health.TakeDamage(_damage);
            }
        }
    }
}