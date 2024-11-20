using UnityEngine;
using System.Collections;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private Transform _enemyAttackPoint;
    [SerializeField] private float _enemyAttackRange = 0.4f;
    [SerializeField] private bool _isAttackEnabled;
    [SerializeField] private int _enemyAttackDamage = 50;

    private float _attackDelay = 1;
    private float _cooldownTime = 1;
    private bool _isAttackPossible = true;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player) && _isAttackEnabled)
        {
            StartCoroutine(AttackWithRate(_cooldownTime, player));
        }
    }

    private IEnumerator AttackWithRate(float delay, Player player)
    {
        var wait = new WaitForSeconds(delay);

        if (_attackDelay > 0)
        {
            yield return wait;

            _attackDelay -= Time.deltaTime;
            Debug.Log("delay is working");
        }
        else
        {
            Debug.Log("enemy hit player");
            Attack(player);
            _attackDelay = _cooldownTime;
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
