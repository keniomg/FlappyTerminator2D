using UnityEngine;
using UnityEngine.Pool;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawnPosition;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private PlayerHealthViewerUIHealthBar _healthBarUI;
    [SerializeField] private PlayerHealthViewerUIText _healthTextUI;
    [SerializeField] private ScenesEventsInvoker _systemEventInvoker;

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
        _player = _pool.Get();
        _player.UnitStatusEventInvoker.Register(_player.gameObject.GetInstanceID(), OnPlayersGameOver);
        _systemEventInvoker.SceneStatusChanged += OnSceneStatusChanged;
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        _player.UnitStatusEventInvoker.Unregister(_player.gameObject.GetInstanceID(), OnPlayersGameOver);
        _systemEventInvoker.SceneStatusChanged -= OnSceneStatusChanged;
    }

    private void AccompanyGet(Player player)
    {
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

    private void AccompanyRelease(Player player) { }

    private void OnPlayersGameOver(GameObject player, UnitStatusTypes statusType)
    {
        if (statusType is UnitStatusTypes.Die)
        {
            Destroy(_player.gameObject);
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