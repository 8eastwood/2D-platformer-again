using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = "Horizontal";

    private KeyCode _jumpKey = KeyCode.Space;
    private KeyCode _attackKey = KeyCode.E;
    private KeyCode _leechAttackKey = KeyCode.Q;

    public bool IsJumpKeyPressed => Input.GetKeyDown(_jumpKey);
    public bool IsAttackKeyPressed => Input.GetKeyDown(_attackKey);
    public bool IsLeechAttackKeyPressed => Input.GetKeyDown(_leechAttackKey);

    public float HorizontalMove { get; private set; }

    private void Update()
    {
        HorizontalMove = Input.GetAxisRaw(Horizontal);
    }
}
