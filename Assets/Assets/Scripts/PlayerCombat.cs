using System.Collections;   
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange = 1f;

    private KeyCode _attackKey = KeyCode.E;
    private bool _isAttackPossible;
    private int _attackDamage = 20;
    private int _delay = 3;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(_attackKey))
        {
            Attack();
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

    private IEnumerator AttackCooldown(int delay)
    {
        var wait = new WaitForSeconds(delay);

        yield return wait;

        _isAttackPossible = true;
    }
}
