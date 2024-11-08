using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawnPosition;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private PlayerHealthViewerUIHealthBar _healthBarUI;
    [SerializeField] private PlayerHealthViewerUIText _healthTextUI;
    [SerializeField] private ScenesEventsInvoker _sceneEventInvoker;
    [SerializeField] private SoundEventsInvoker _soundEventsInvoker;

    private Player _player;

    private void OnEnable()
    {
        _player = Instantiate(_playerPrefab);
        OnPlayerEnable(_player);
    }

    private void OnPlayerEnable(Player player)
    {
        player.GetComponents();
        player.InitializeComponents(_sceneEventInvoker, _soundEventsInvoker);
        _healthBarUI.Initialize(player.PlayerHealth);
        _healthTextUI.Initialize(player.PlayerHealth);
        player.transform.position = _playerSpawnPosition.position;

        if (player.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.ResetValue();
        }
    }
}