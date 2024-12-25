using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _sprite;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed = 4f;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float _jumpingPower = 20f;

    public readonly int Jumping = Animator.StringToHash(nameof(Jumping));
    public readonly int Speed = Animator.StringToHash(nameof(Speed));
    private Quaternion _inverseRotation = Quaternion.Euler(0, 180, 0);
    private float _radius = 0.2f;
    private bool _isJump;

    private void Awake()
    {
        //StartCoroutine(CheckGround());
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = new Vector2(_inputReader.HorizontalMove * _speed, _rigidBody.velocity.y);

        if (_isJump)
        {
            //_rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpingPower);
            //StartCoroutine()
            _animator.SetBool(Jumping, true);
            Debug.Log("ןנûד");

            //if (IsGrounded())
            //{
            //    _isJump = false;
            //}
        }

        if (_isJump == false)
        {
            _animator.SetBool(Jumping, false);
        }
    }

    private void Update()
    {
        _animator.SetFloat(Speed, Mathf.Abs(_inputReader.HorizontalMove));

            Debug.Log(_inputReader.IsJumpKeyPressed);
        if (_inputReader.IsJumpKeyPressed && IsGrounded())
        {
            Jump();
        }

        FlipSide();
    }

    private void Jump()
    {
        _rigidBody.AddForce(Vector2.up * _jumpingPower, ForceMode2D.Impulse);

        //StartCoroutine(CheckGround());
    }

    private IEnumerator CheckGround()
    {
        while (enabled)
        {
            IsGrounded();
        }

        yield return new WaitUntil(IsGrounded);

        //_isJump = false;
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