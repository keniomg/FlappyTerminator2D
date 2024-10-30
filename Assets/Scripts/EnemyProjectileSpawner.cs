using System.Collections;
using UnityEngine;

public class EnemyProjectileSpawner : ProjectileSpawner
{
    [SerializeField] private float _cooldownTime;
    [SerializeField] private float _maximumAttackTime;

    private Coroutine _spawnCycle;
    private WaitForSeconds _cooldown;

    protected override void Awake()
    {
        base.Awake();

        Direction = Vector2.left;
        _spawnCycle = null;
        _cooldown = new(_cooldownTime);
    }

    protected override void FixedUpdate()
    {
        if (_spawnCycle == null)
        {
            _spawnCycle = StartCoroutine(ProjectileSpawnCycle());
        }

        base.FixedUpdate();
    }

    private void OnEnable()
    {
        _spawnCycle = null;
        Spawn = null;
        IsAttackAvailable = true;
    }

    private IEnumerator ProjectileSpawnCycle()
    {
        float currentAttackTime = 0;

        while (currentAttackTime < _maximumAttackTime)
        {
            currentAttackTime += Time.deltaTime;
            yield return Time.deltaTime;
        }

        IsAttackAvailable = false;

        yield return _cooldown;
        
        IsAttackAvailable = true;
        _spawnCycle = null;
    }
}