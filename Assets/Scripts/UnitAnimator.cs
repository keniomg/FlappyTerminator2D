using UnityEngine;

public abstract class UnitAnimator : MonoBehaviour
{
    private Animator _animator;
    private UnitStatus _unitStatus;

    private void Awake()
    {
        _animator = TryGetComponent(out Animator animator) ? animator : null;
    }

    public void HandleAnimation()
    {
        _animator.SetBool(UnitStatusTypes.Attack.ToString(), _unitStatus.IsAttack);
        _animator.SetBool(UnitStatusTypes.Damaged.ToString(), _unitStatus.IsDamaged);
        _animator.SetBool(UnitStatusTypes.Died.ToString(), _unitStatus.IsDied);
    }

    public void Initialize(UnitStatus unitStatus)
    {
        _unitStatus = unitStatus;
    }
}