using UnityEngine;

public class Mover : MonoBehaviour
{
    private const string Horizontal = "Horizontal";

    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _sprite;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed = 4f;

    public readonly int Jumping = Animator.StringToHash(nameof(Jumping));
    public readonly int Speed = Animator.StringToHash(nameof(Speed));
    private Quaternion _inverseRotation = Quaternion.Euler(0, 180, 0);
    private KeyCode _jumpKey = KeyCode.Space;
    private float _horizontalMove;
    private float _jumpingPower = 8f;
    private float _radius = 0.2f;
    private bool _isJump;
    private bool _isFacingCorrect = true;

    private void FixedUpdate()
    {
        _rigidBody.velocity = new Vector2(_horizontalMove * _speed, _rigidBody.velocity.y);

        if (_isJump)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpingPower);
            _animator.SetBool(Jumping, true);
            Debug.Log("ןנûד");

            if (IsGrounded())
            {
                _isJump = false;

            }
        }

        if (_isJump == false)
        {
            _animator.SetBool(Jumping, false);
        }
    }

    private void Update()
    {
        _horizontalMove = Input.GetAxisRaw(Horizontal);
        _animator.SetFloat(Speed, Mathf.Abs(_horizontalMove));

        if (Input.GetKeyDown(_jumpKey) && IsGrounded())
        {
            _isJump = true;
        }

        FlipSide();
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, _radius, _groundLayer);
    }

    private void FlipSide()
    {
        if (_horizontalMove < 0f)
        {
            _sprite.rotation = _inverseRotation;
        }
        else if (_horizontalMove > 0f)
        {
            _sprite.rotation = Quaternion.identity;
        }
    }
}