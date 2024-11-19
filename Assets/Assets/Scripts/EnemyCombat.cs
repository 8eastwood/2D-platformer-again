using UnityEngine;
using System.Collections;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private Transform _enemyAttackPoint;
    [SerializeField] private float _enemyAttackRange = 0.4f;
    [SerializeField] private bool _isAttackEnabled;
    //[SerializeField] private Cooldown _cooldown;

    private float _attackDelay;
    private float _cooldown = 3;
    private int _enemyAttackDamage = 1;

    private void Awake()
    {
        _attackDelay = _cooldown;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player) && _isAttackEnabled)
        {
            Attack(player);
            Debug.Log("enemy hit player");
            _attackDelay = _cooldown;

            while (_attackDelay > 0)
            {
                Debug.Log("delay is working");
                _attackDelay -= Time.deltaTime;
            }
        }
    }

    private IEnumerator AttackCooldown(int delay)
    {
        var wait = new WaitForSeconds(delay);

        Debug.Log("delay is working");

        yield return wait;
    }

    private void Attack(Player player)
    {
        player.TakeDamage(_enemyAttackDamage);
        //StartCoroutine(AttackCooldown(_attackDelay));
    }

    private void OnDrawGizmosSelected()
    {
        if (_enemyAttackPoint == null)
            return;

        Gizmos.DrawWireSphere(_enemyAttackPoint.position, _enemyAttackRange);
    }
}
