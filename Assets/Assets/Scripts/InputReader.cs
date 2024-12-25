using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = "Horizontal";

    private KeyCode _jumpKey = KeyCode.Space;
    private float _horizontalMove;
    private bool _isJumpKeyPressed = false;

    //public bool IsJumpKeyPressed => _isJumpKeyPressed;
    public float HorizontalMove => _horizontalMove;

    private void Update()
    {
        _horizontalMove = Input.GetAxisRaw(Horizontal);
    }

    public bool IsJumpKeyPressed()
    {
        if (Input.GetKeyDown(_jumpKey))
        {
            _isJumpKeyPressed = true;
        }

        return _isJumpKeyPressed;
    }
}
