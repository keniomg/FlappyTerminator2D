using UnityEngine;

public abstract class UnitAnimator : MonoBehaviour
{
    private Animator _animator;
    private UnitStatus _unitStatus;

    private void Awake()
    {
        _animator = TryGetComponent(out Animator animator) ? animator : null;
    }

    public abstract void HandleAnimation();

    public void Initialize(UnitStatus unitStatus)
    {
        _unitStatus = unitStatus;
    }
}