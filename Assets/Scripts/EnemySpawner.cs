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

    private Vector2 _moveDirection;
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

    private void AccompanyGet(Enemy enemyPrefab)
    {
        enemyPrefab.gameObject.SetActive(true);
        enemyPrefab.UnitStatusEventInvoker.Register(enemyPrefab.gameObject.GetInstanceID(), OnEnemyHealthOver);
        SetPosition(enemyPrefab.gameObject);

        if (enemyPrefab.gameObject.TryGetComponent(out EnemyMover enemyMover))
        {
            enemyMover.Initialize(_moveDirection);
        }

        if (enemyPrefab.gameObject.TryGetComponent(out EnemyCollisionHandler collisionHandler))
        {
            collisionHandler.Initialize(_owner);
            _eventInvoker.Register(enemyPrefab.gameObject.GetInstanceID(), OnEnemyCollidedSomething);
        }
    }

    private void AccompanyRelease(Enemy enemyPrefab)
    {
        enemyPrefab.gameObject.SetActive(false);
        enemyPrefab.UnitStatusEventInvoker.Unregister(enemyPrefab.gameObject.GetInstanceID(), OnEnemyHealthOver);

        if (enemyPrefab.gameObject.TryGetComponent(out EnemyCollisionHandler collisionHandler))
        {
            _eventInvoker.Unregister(enemyPrefab.gameObject.GetInstanceID(), OnEnemyCollidedSomething);
        }
    }

    private void SetPosition(GameObject gameObject)
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

        gameObject.transform.position = new(spawnPositionX, spawnPositionY);
    }

    private void OnEnemyCollidedSomething(GameObject enemyObject, GameObject other)
    {
        if (enemyObject.TryGetComponent(out Enemy enemy) && other.TryGetComponent(out EnemyBorder enemyBorder))
        {
            _pool.Release(enemy);
        }
    }

    private void OnEnemyHealthOver(GameObject enemyObject, UnitStatusTypes unitStatusType)
    {
        if (enemyObject.TryGetComponent(out Enemy enemy))
        {
            if (unitStatusType is UnitStatusTypes.Die)
            {
                _pool.Release(enemy);
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
}