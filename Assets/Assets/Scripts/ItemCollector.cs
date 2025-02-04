using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Wallet _wallet;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Coin coin))
        {
            _wallet.AddCoin();
            coin.DestroyAfterCapture();
        }
        else if (other.gameObject.TryGetComponent(out HealEssense healEssense))
        {
            _player.Heal(healEssense.HealPoints);
            healEssense.DestroyAfterCapture();
        }
    }
}