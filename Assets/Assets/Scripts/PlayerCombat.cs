using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Transform _leechAttackPoint;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Player _player;
    [SerializeField] private float _leechAttackRange = 2f;
    [SerializeField] private float _attackRange = 1f;

    private Coroutine _leechAttackCoroutine;
    private KeyCode _leechAttackKey = KeyCode.Q;
    private KeyCode _attackKey = KeyCode.E;
    private float _leechAttackDuration = 6;
    private bool _isAttackPossible = true;
    private int _leechAttackDamage = 7;
    private int _attackDamage = 10;
    private int _leechDelay = 4;
    private int _delay = 1;

    private void Update()
    {
        if (Input.GetKeyDown(_attackKey) && _isAttackPossible)
        {
            Attack();
        }
        else if (Input.GetKeyDown(_leechAttackKey) && _isAttackPossible)
        {
            _leechAttackCoroutine = StartCoroutine(LeechAttack());
        }
    }

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayer);

        foreach (Collider2D hit in hitEnemies)
        {
            if (hit.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_attackDamage);
                _isAttackPossible = false;
            }
        }

        StartCoroutine(AttackCooldown(_delay));
    }

    private IEnumerator LeechAttack()
    {
        Collider2D[] hitEnemies = new Collider2D[1];
        int countColliders = Physics2D.OverlapCircleNonAlloc(_leechAttackPoint.position, _leechAttackRange, hitEnemies, _enemyLayer);
        Collider2D collider = hitEnemies[0];
        float currentTime = 0;
        var waitForSecond = new WaitForSeconds(1);

        while (currentTime <= _leechAttackDuration && collider != null)
        {
            if (collider.TryGetComponent(out Enemy enemy))
            {
                int enemyHealthBeforeAttack = enemy.Health.CurrentAmount;
                enemy.TakeDamage(_leechAttackDamage);
                _player.Health.TakeHeal(enemyHealthBeforeAttack - enemy.Health.CurrentAmount);
                _isAttackPossible = false;
            }

            currentTime++;

            yield return waitForSecond;
        }

        currentTime = 0;

        StartCoroutine(AttackCooldown(_leechDelay));
    }

    private IEnumerator AttackCooldown(int delay)
    {
        var wait = new WaitForSeconds(delay);
        Debug.Log("ждем ");

        yield return wait;

        _isAttackPossible = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (_leechAttackPoint == null)
            return;

        Gizmos.DrawWireSphere(_leechAttackPoint.position, _leechAttackRange);
    }
}
