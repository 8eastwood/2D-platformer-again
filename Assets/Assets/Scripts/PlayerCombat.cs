using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Transform _leechAttackPoint;
    [SerializeField] private Player _player;
    [SerializeField] private float _attackRange = 1f;
    [SerializeField] private float _leechAttackRange = 2f;

    private KeyCode _attackKey = KeyCode.E;
    private KeyCode _leechAttackKey = KeyCode.Q;
    private float _leechAttackDuration = 4;
    //private float _leechAttackDurationValue = 4;
    private bool _isAttackPossible = true;
    private int _attackDamage = 10;
    private int _leechAttackDamage = 2;
    private int _delay = 1;
    //private int _leechDelay = 6;
    private Coroutine _cooldownCoroutine;
    private Coroutine _leechAttackCoroutine;


    private void FixedUpdate()
    {
        if (Input.GetKeyDown(_attackKey) && _isAttackPossible)
        {
            Debug.Log("бьем");

            Attack();
        }
        else if (Input.GetKeyDown(_leechAttackKey) && _isAttackPossible)
        {
            _leechAttackCoroutine = StartCoroutine(LeechAttack());
            //LeechAttackDurationReset();
        }
    }

    private void Update()
    {
        if (_isAttackPossible)
        {
            Debug.Log("можно жахнуть");
        }
        else
        {
            Debug.Log("нельз€");
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
        Debug.Log("сосем");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_leechAttackPoint.position, _leechAttackRange, _enemyLayer);
        float wait = 0;

        while (wait != _leechAttackDuration)
        {
            foreach (Collider2D hit in hitEnemies)
            {
                if (hit.TryGetComponent(out Enemy enemy))
                {
                    int enemyHealthBeforeAttack = enemy.Health.CurrentAmount;
                    enemy.TakeDamage(_leechAttackDamage);
                    _player.Health.TakeHeal(enemyHealthBeforeAttack - enemy.Health.CurrentAmount);
                    _isAttackPossible = false;
                    _leechAttackCoroutine = null;
                }
            }

            wait = Mathf.MoveTowards(wait, _leechAttackDuration, Time.deltaTime);

            yield return wait;
        }

        //_leechAttackDuration -= Time.deltaTime;
        //Debug.Log(" врем€ атаки = " + _leechAttackDuration);

        //_cooldownCoroutine = StartCoroutine(AttackCooldown(_leechDelay));
    }

    private IEnumerator AttackCooldown(int delay)
    {
        var wait = new WaitForSeconds(delay);
        Debug.Log("ждем ");

        yield return wait;

        _isAttackPossible = true;
    }

    //private void LeechAttackDurationReset()
    //{
    //    _leechAttackDuration = _leechAttackDurationValue;
    //}

    private void OnDrawGizmosSelected()
    {
        if (_leechAttackPoint == null)
            return;

        Gizmos.DrawWireSphere(_leechAttackPoint.position, _leechAttackRange);
    }
}
