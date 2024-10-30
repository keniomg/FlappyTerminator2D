using UnityEngine;

public class AttackerData : MonoBehaviour
{
    [field: SerializeField] public float ProjectilesPerSecond { get; private set; }
    [field: SerializeField] public float ProjectileSpeed { get; private set; }
    [field: SerializeField] public int CollisionDamage { get; private set; }
    [field: SerializeField] public int ProjectileDamage { get; private set; }
    [field: SerializeField] public LayerMask AttackOwner { get; private set; }
}