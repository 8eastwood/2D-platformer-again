using UnityEngine;
using System.Collections;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private Transform _enemyAttackPoint;
    [SerializeField] private bool _isAttackEnabled = true;
    [SerializeField] private int _enemyAttackDamage = 50;

    private float _attackDelay = 0.3f;
    private float _cooldownTime = 1;

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
        }
        else
        {
            Attack(player);
            _attackDelay = _cooldownTime;
        }
    }

    private void Attack(Player player)
    {
        player.TakeDamage(_enemyAttackDamage);
    }
}
