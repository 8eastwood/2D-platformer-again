using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private Transform _enemyAttackPoint;
    [SerializeField] private float _enemyAttackRange = 0.4f;
    [SerializeField] private bool _isAttackEnabled;

    private int _enemyAttackDamage = 1;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player) && _isAttackEnabled)
        {
            Attack(player);
        }
    }

    private void Attack(Player player)
    {
        player.TakeDamage(_enemyAttackDamage);
    }

    private void OnDrawGizmosSelected()
    {
        if (_enemyAttackPoint == null)
            return;

        Gizmos.DrawWireSphere(_enemyAttackPoint.position, _enemyAttackRange);
    }
}
