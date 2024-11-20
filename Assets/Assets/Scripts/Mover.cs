using UnityEngine;

public class Mover : MonoBehaviour
{
    private const string Horizontal = "Horizontal";

    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed = 4f;
    
    public readonly int Speed = Animator.StringToHash(nameof(Speed));
    public readonly int Jumping = Animator.StringToHash(nameof(Jumping));
    private KeyCode _jumpKey = KeyCode.Space;
    private float _horizontalMove;
    private float _jumpingPower = 8f;
    private bool _isFacingCorrect = true;
    private bool _isJump;

    private void FixedUpdate()
    {
        _rigidBody.velocity = new Vector2(_horizontalMove * _speed, _rigidBody.velocity.y);

        if (_isJump)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpingPower);
            _animator.SetBool(Jumping, true);
            _isJump = false;
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
        return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
    }

    private void FlipSide()
    {
        float sideChanger = -1f;

        if (_isFacingCorrect && _horizontalMove < 0f || !_isFacingCorrect && _horizontalMove > 0f)
        {
            _isFacingCorrect = !_isFacingCorrect;
            Vector3 localScale = transform.localScale;
            localScale.x *= sideChanger;
            transform.localScale = localScale;
        }
    }
}