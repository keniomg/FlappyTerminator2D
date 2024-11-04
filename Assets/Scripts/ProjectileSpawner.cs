using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private Projectile _projectile;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaximumSize;
    [SerializeField] private Transform _spawnTransform;
    [SerializeField] private GeneralCollisionEventInvoker _collisionEventInvoker;
    
    protected Vector2 Direction;
    protected Coroutine Spawn;
    protected bool IsAttackAvailable;

    private SoundEventsInvoker _soundEventInvoker;
    private const float SecondsInOneSecond = 1;
    private float _spawnDelay;
    private AttackerData _attackerData;
    private UnitStatusEventInvoker _unitStatusEventInvoker;
    private WaitForSeconds _delay;
    private ObjectPool<Projectile> _pool;

    public event Action<GameObject, UnitStatusTypes> Attacks;

    protected virtual void Awake()
    {
        Spawn = null;
        IsAttackAvailable = true;

        _pool = new ObjectPool<Projectile>(
                createFunc: () => Instantiate(_projectile),
                actionOnGet: (projectile) => AccompanyGet(projectile),
                actionOnRelease: (projectile) => AccompanyRelease(projectile),
                actionOnDestroy: (projectile) => Destroy(projectile),
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaximumSize);
    }

    protected virtual void FixedUpdate()
    {
        if (IsAttackAvailable && Spawn == null)
        {
            Spawn = StartCoroutine(SpawnProjectile());
        }
    }

    private void OnDisable()
    {
        _unitStatusEventInvoker.Unregister(gameObject.GetInstanceID(), Attacks);
    }

    public void Initialize(UnitStatusEventInvoker unitStatusEventInvoker, AttackerData attackerData, SoundEventsInvoker soundEventsInvoker)
    {
        _unitStatusEventInvoker = unitStatusEventInvoker;
        _attackerData = attackerData;
        _spawnDelay = SecondsInOneSecond / _attackerData.ProjectilesPerSecond;
        _delay = new(_spawnDelay);
        _unitStatusEventInvoker.Register(gameObject.GetInstanceID(), Attacks);
        _soundEventInvoker = soundEventsInvoker;
    }

    private void AccompanyGet(Projectile projectile)
    {
        projectile.Initialize(Direction, _collisionEventInvoker, _attackerData);
        projectile.gameObject.SetActive(true);
        SetDefaultVelocityAndRotation(projectile);
        projectile.transform.position = _spawnTransform.position;
        _collisionEventInvoker.Register(projectile.gameObject.GetInstanceID(), OnProjectileCollidedSomething);
        _soundEventInvoker.Invoke(SoundTypes.ProjectileSpawned);
    }

    private void AccompanyRelease(Projectile projectile)
    {
        _collisionEventInvoker.Unregister(projectile.gameObject.GetInstanceID(), OnProjectileCollidedSomething);
        projectile.gameObject.SetActive(false);
        _soundEventInvoker.Invoke(SoundTypes.ProjectileCollided);
    }

    private void OnProjectileCollidedSomething(GameObject projectileObject, GameObject otherObject)
    {
        if (projectileObject.TryGetComponent(out Projectile projectile))
        {
            _pool.Release(projectile);
        }
    }

    private void SetDefaultVelocityAndRotation(Projectile projectile)
    {
        projectile.transform.rotation = Quaternion.identity;

        if (projectile.TryGetComponent(out Rigidbody2D rigidbody))
        {
            rigidbody.velocity = Vector2.zero;
        }
    }

    private IEnumerator SpawnProjectile()
    {
        _pool.Get();
        _unitStatusEventInvoker.Invoke(gameObject.GetInstanceID(), gameObject, UnitStatusTypes.Attack);

        yield return _delay;

        Spawn = null;
    }
}