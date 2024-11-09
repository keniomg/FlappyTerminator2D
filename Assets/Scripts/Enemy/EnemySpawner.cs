using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform _spawnZone;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaximumSize;
    [SerializeField] private LayerMask _owner;
    [SerializeField] private GeneralCollisionEventInvoker _eventInvoker;
    [SerializeField] private ScoreEventInvoker _scoreInvoker;
    [SerializeField] private SoundEventsInvoker _soundEventsInvoker;

    private Coroutine _dyingCoroutine;
    private Vector2 _moveDirection;
    private WaitForSeconds _dieAnimationDelay;
    private WaitForSeconds _delay;
    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _moveDirection = Vector2.left;
        _delay = new(_spawnDelay);

        _pool = new ObjectPool<Enemy>(
               createFunc: () => Instantiate(_enemyPrefab),
               actionOnGet: (enemyPrefab) => AccompanyGet(enemyPrefab),
               actionOnRelease: (enemyPrefab) => AccompanyRelease(enemyPrefab),
               actionOnDestroy: (enemyPrefab) => Destroy(enemyPrefab),
               collectionCheck: true,
               defaultCapacity: _poolCapacity,
               maxSize: _poolMaximumSize);
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private void AccompanyGet(Enemy enemy)
    {
        enemy.GetComponents();
        enemy.InitializeComponents(_soundEventsInvoker);
        enemy.UnitStatusEventInvoker.Register(enemy.gameObject.GetInstanceID(), OnEnemyHealthChanged);
        SetPosition(enemy.gameObject);

        if (enemy.gameObject.TryGetComponent(out EnemyMover enemyMover))
        {
            enemyMover.Initialize(_moveDirection);
        }

        if (enemy.gameObject.TryGetComponent(out EnemyCollisionHandler collisionHandler))
        {
            collisionHandler.Initialize(_owner, _soundEventsInvoker);
            _eventInvoker.Register(enemy.gameObject.GetInstanceID(), OnEnemyCollidedBorder);
        }

        enemy.gameObject.SetActive(true);
    }

    private void AccompanyRelease(Enemy enemy)
    {
        enemy.UnitStatusEventInvoker.Unregister(enemy.gameObject.GetInstanceID(), OnEnemyHealthChanged);

        if (enemy.gameObject.TryGetComponent(out EnemyCollisionHandler collisionHandler))
        {
            _eventInvoker.Unregister(enemy.gameObject.GetInstanceID(), OnEnemyCollidedBorder);
        }

        enemy.gameObject.SetActive(false);
    }

    private void SetPosition(GameObject enemyObject)
    {
        const int NumbersOfWidthSide = 2;
        const int NumbersOfHeightSide = 2;

        float xSpawnPositionLength = _spawnZone.transform.localScale.x / NumbersOfWidthSide;
        float ySpawnPositionLength = _spawnZone.transform.localScale.y / NumbersOfHeightSide;
        float xSpawnPositionOffsetLeft = _spawnZone.transform.position.x - xSpawnPositionLength;
        float xSpawnPositionOffsetRight = _spawnZone.transform.position.x + xSpawnPositionLength;
        float ySpawnPositionOffsetDown = _spawnZone.transform.position.y - ySpawnPositionLength;
        float ySpawnPositionOffsetUp = _spawnZone.transform.position.y + ySpawnPositionLength;

        float spawnPositionX = Random.Range(xSpawnPositionOffsetLeft, xSpawnPositionOffsetRight);
        float spawnPositionY = Random.Range(ySpawnPositionOffsetDown, ySpawnPositionOffsetUp);

        enemyObject.transform.position = new(spawnPositionX, spawnPositionY);
    }

    private void OnEnemyCollidedBorder(GameObject enemyObject, GameObject other)
    {
        if (enemyObject.TryGetComponent(out Enemy enemy) && other.TryGetComponent(out EnemyBorder enemyBorder))
        {
            _pool.Release(enemy);
        }
    }

    private void OnEnemyHealthChanged(GameObject enemyObject, UnitStatusTypes unitStatusType)
    {
        if (enemyObject.TryGetComponent(out Enemy enemy))
        {
            if (unitStatusType is UnitStatusTypes.Died)
            {
                _scoreInvoker.Invoke(enemy.KillAward);

                if (_dyingCoroutine == null)
                {
                    _dyingCoroutine = StartCoroutine(ReleaseOnDestroyed(enemy));
                }
            }
        }
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            _pool.Get();

            yield return _delay;
        }
    }

    private IEnumerator ReleaseOnDestroyed(Enemy enemy)
    {
        if (enemy.TryGetComponent(out EnemyAnimator enemyAnimator))
        {
            _dieAnimationDelay = new(enemyAnimator.DyingAnimation.length);

            yield return _dieAnimationDelay;

            _pool.Release(enemy);
            _dyingCoroutine = null;
        }
    }
}