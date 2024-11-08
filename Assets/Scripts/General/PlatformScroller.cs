using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlatformScroller : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed;
    [SerializeField] private float _defaultPositionX;
    [SerializeField] private float _minimumPositionX;
    [SerializeField] private ScenesEventsInvoker _scenesEventInvoker;

    private void Update()
    {
        transform.position += Vector3.left * _scrollSpeed * Time.deltaTime;

        if (transform.position.x < _minimumPositionX)
        {
            transform.position = new(_defaultPositionX, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            _scenesEventInvoker.Invoke(ScenesEventsTypes.PlayerTouchedPlatform);
        }
    }
}