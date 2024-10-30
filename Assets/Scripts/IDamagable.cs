using UnityEngine;

public interface IDamagable
{
    public void TakeDamage(int decreaseValue, LayerMask attacker);
}