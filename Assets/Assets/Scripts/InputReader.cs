using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = "Horizontal";

    private KeyCode _jumpKey = KeyCode.Space;

    public bool IsJumpKeyPressed => Input.GetKeyDown(_jumpKey);
    public float HorizontalMove { get; private set; }

    private void Update()
    {
        HorizontalMove = Input.GetAxisRaw(Horizontal);
    }
}
