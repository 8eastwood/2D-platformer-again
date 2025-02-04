using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _sprite;
    [SerializeField] private float _jumpingPower = 20f;
    [SerializeField] private float _speed = 4f;

    private Quaternion _inverseRotation = Quaternion.Euler(0, 180, 0);
    private float _radius = 0.2f;

    private void FixedUpdate()
    {
        _rigidBody.velocity = new Vector2(_inputReader.HorizontalMove * _speed, _rigidBody.velocity.y);
    }

    private void Update()
    {
        if (Mathf.Abs(_inputReader.HorizontalMove) > 0)
        {
            _playerAnimator.PlayRunAnimation(_inputReader.HorizontalMove);
        }
        else
        {
            _playerAnimator.PlayIdleAnimation();
        }

        if (_inputReader.IsJumpKeyPressed && IsGrounded())
        {
            Jump();
            _playerAnimator.PlayJumpAnimation();
        }

        FlipSide();
    }

    private void Jump()
    {
        _rigidBody.AddForce(Vector2.up * _jumpingPower, ForceMode2D.Impulse);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, _radius, _groundLayer);
    }

    private void FlipSide()
    {
        if (_inputReader.HorizontalMove < 0f)
        {
            _sprite.rotation = _inverseRotation;
        }
        else if (_inputReader.HorizontalMove > 0f)
        {
            _sprite.rotation = Quaternion.identity;
        }
    }
}