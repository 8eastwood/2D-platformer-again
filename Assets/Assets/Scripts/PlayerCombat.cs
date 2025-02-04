using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Ability _ability;
    [SerializeField] private Player _player;
    [SerializeField] private float _attackRange = 1f;

    private WaitForSeconds _delay;
    private bool _isAttackPossible = true;
    private int _attackDamage = 10;
    private int _amountOfSeconds = 1;

    public LayerMask EnemyLayer => _enemyLayer;

    private void Awake()
    {
        _delay = new WaitForSeconds(_amountOfSeconds);
    }

    private void Update()
    {
        if (_inputReader.IsAttackKeyPressed && _isAttackPossible)
        {
            Attack();
        }
        else if (_inputReader.IsLeechAttackKeyPressed )
        {
            _ability.TryStart(_enemyLayer);
            //StartCoroutine(_ability.LeechAttack(_player, this));
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

    public IEnumerator AttackCooldown(WaitForSeconds wait)
    {
        yield return wait;

        _isAttackPossible = true;
    }
}
