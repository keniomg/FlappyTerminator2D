using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerBorder : UnitBorder
{
    [SerializeField] private float _pushBackOffsetX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerHealth playerHealth))
        {
            Vector2 playerPosition = collision.gameObject.transform.position;
            playerPosition.x += _pushBackOffsetX;
            collision.gameObject.transform.position = playerPosition;
        }
    }
}