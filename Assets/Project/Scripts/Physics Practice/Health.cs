using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] private int _maximumValue;
    
    private int _currentValue;

    private void Awake()
    {
        _currentValue = _maximumValue;
    }

    internal void TakeDamage(int damage)
    {
        if (_currentValue > 0)
        {
            if (damage > 0)
            {
                _currentValue -= damage;
                _currentValue = Mathf.Clamp(_currentValue, 0, _maximumValue);
            }
        }

        if (_currentValue == 0)
        {
            Die();
        }
    }

    internal abstract void Die();
}