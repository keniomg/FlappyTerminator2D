using UnityEngine;
using UnityEngine.Pool;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawnPosition;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private PlayerHealthViewerUIHealthBar _healthBarUI;
    [SerializeField] private PlayerHealthViewerUIText _healthTextUI;
    [SerializeField] private ScenesEventsInvoker _sceneEventInvoker;

    private ObjectPool<Player> _pool;
    private Player _player;
    private int _poolCapacity;
    private int _poolMaximumSize;

    private void Awake()
    {
        _poolCapacity = 1;
        _poolMaximumSize = 1;

        _pool = new ObjectPool<Player>(
                createFunc: () => Instantiate(_playerPrefab),
                actionOnGet: (player) => AccompanyGet(player),
                actionOnRelease: (player) => AccompanyRelease(player),
                actionOnDestroy: (player) => Destroy(player),
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaximumSize);
    }

    private void Start()
    {
        _pool.Get();
        _sceneEventInvoker.SceneStatusChanged += OnSceneStatusChanged;
    }

    private void OnDisable()
    {
        _sceneEventInvoker.SceneStatusChanged -= OnSceneStatusChanged;
    }

    private void AccompanyGet(Player player)
    {
        player.UnitStatusEventInvoker.Register(_player.gameObject.GetInstanceID(), OnPlayersGameOver);

        if (player.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.ResetValue();
        }

        player.transform.position = _playerSpawnPosition.position;
        player.GetComponents();
        player.InitializeComponents();
        _healthBarUI.Initialize(player.PlayerHealth);
        _healthTextUI.Initialize(player.PlayerHealth);
    }

    private void AccompanyRelease(Player player) 
    {
        player.UnitStatusEventInvoker.Unregister(_player.gameObject.GetInstanceID(), OnPlayersGameOver);
    }

    private void OnPlayersGameOver(GameObject playerObject, UnitStatusTypes statusType)
    {
        if (statusType is UnitStatusTypes.Die)
        {
            _sceneEventInvoker.Invoke(ScenesEventsTypes.PlayerDied);

            if (playerObject.TryGetComponent(out Player player))
            {
                _pool.Release(player);
            }
        }
    }

    private void OnSceneStatusChanged(ScenesEventsTypes sceneEventType)
    {
        if (sceneEventType is ScenesEventsTypes.OpenedMainMenu ||
            sceneEventType is ScenesEventsTypes.PlayerDied)
        {
            _pool.Release(_player);
        }
        else if (sceneEventType is ScenesEventsTypes.GameStarted)
        {
            _pool.Get(out _player);
        }
        else if (sceneEventType is ScenesEventsTypes.GameRestarted)
        {
            _pool.Release(_player);
            _pool.Get(out _player);
        }
    }
}