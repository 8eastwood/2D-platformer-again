using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private Player _player;

    public Transform _playerPosition { get; private set; }
    public bool IsPlayerNear { get; private set; } = false;

    public event Action<Player> PlayerEntered;
    public event Action PlayerEscaped;

    private Transform TransferPlayerPosition(Transform playerPosition)
    {
        return playerPosition;
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
}
