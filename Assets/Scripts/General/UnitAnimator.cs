using UnityEngine;

public abstract class UnitAnimator : MonoBehaviour
{
    [field: SerializeField] public AnimationClip DyingAnimation {get; private set; }

    private Animator _animator;
    private UnitStatus _unitStatus;

    private void Awake()
    {
        _animator = TryGetComponent(out Animator animator) ? animator : null;
        _animator.keepAnimatorStateOnDisable = true;
    }

    private void OnDisable()
    {
        ResetAnimation();
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

    public void ResetAnimation()
    {
        _animator.SetBool(UnitStatusTypes.Attack.ToString(), false);
        _animator.SetBool(UnitStatusTypes.Damaged.ToString(), false);
        _animator.SetBool(UnitStatusTypes.Died.ToString(), false);
    }
}