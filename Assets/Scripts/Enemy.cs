using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMover), typeof(EnemyCollisionHandler), typeof(EnemyProjectileSpawner))]
[RequireComponent(typeof(EnemyHealth), typeof(Rigidbody2D), typeof(Collider2D))]
[RequireComponent(typeof(AttackerData), typeof(EnemyStatus), typeof(EnemyAnimator))]
[RequireComponent (typeof(Animator))]

public class Enemy : Unit
{
    private EnemyMover _enemyMover;
    private EnemyHealth _enemyHealth;
    private EnemyCollisionHandler _enemyCollisionHandler;
    private EnemyProjectileSpawner _enemyProjectileSpawner;
    private Rigidbody2D _rigidbody;
    private AttackerData _attacker;
    private EnemyStatus _enemyStatus;
    private EnemyAnimator _enemyAnimator;

    [field: SerializeField] public int KillAward {get; private set; }

    public UnitStatusEventInvoker UnitStatusEventInvoker { get; private set; }

    private void Awake()
    {
        GetComponents();
        InitializeComponents();
    }

    private void FixedUpdate()
    {
        _enemyMover.Move();
    }

    private void GetComponents()
    {
        UnitStatusEventInvoker = ScriptableObject.CreateInstance<UnitStatusEventInvoker>();

        _enemyMover = GetComponent<EnemyMover>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _enemyCollisionHandler = GetComponent<EnemyCollisionHandler>();
        _enemyProjectileSpawner = GetComponent<EnemyProjectileSpawner>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _attacker = GetComponent<AttackerData>();
        _enemyAnimator = GetComponent<EnemyAnimator>();
        _enemyStatus = GetComponent<EnemyStatus>();
    }

    private void InitializeComponents()
    {
        _enemyHealth.Initialize(UnitStatusEventInvoker);
        _enemyStatus.Initialize(UnitStatusEventInvoker);
        _enemyAnimator.Initialize(_enemyStatus);
        _enemyProjectileSpawner.Initialize(UnitStatusEventInvoker, _attacker);
    }
}