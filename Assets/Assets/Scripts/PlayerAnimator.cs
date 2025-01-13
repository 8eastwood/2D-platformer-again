using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private readonly int Jumping = Animator.StringToHash(nameof(Jumping));
    private readonly int Speed = Animator.StringToHash(nameof(Speed));
    private readonly int Idle = Animator.StringToHash(nameof(Idle));

    public void PlayJumpAnimation()
    {
        _animator.SetBool(Idle, false);
        _animator.SetTrigger(Jumping);
    }

    public void PlayRunAnimation(float speed)
    {
        _animator.SetBool(Idle, false);
        _animator.SetFloat(Speed, Mathf.Abs(speed));
    }

    public void PlayIdleAnimation()
    {
        _animator.SetFloat(Speed, 0);
        _animator.SetBool(Idle, true);
    }
}
