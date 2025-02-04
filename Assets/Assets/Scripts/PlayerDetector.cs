using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerDetector : MonoBehaviour
{
    public event Action<Player> PlayerEntered;
    public event Action PlayerEscaped;

    public bool IsPlayerNear { get; private set; } = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            DetectPlayerNear(player);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            DetectPlayerLeft();
        }
    }

    private void DetectPlayerNear(Player player)
    {
        PlayerEntered?.Invoke(player);
        IsPlayerNear = true;
    }

    private void DetectPlayerLeft()
    {
        PlayerEscaped?.Invoke();
        IsPlayerNear = false;
    }
}
