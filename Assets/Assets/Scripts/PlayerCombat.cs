using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Transform _leechAttackPoint;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Player _player;
    [SerializeField] private float _leechAttackRange = 2f;
    [SerializeField] private float _attackRange = 1f;
    [SerializeField] private Ability _ability;
    [SerializeField] private Cooldown _cooldown;

    private Coroutine _leechAttackCoroutine;
    private KeyCode _leechAttackKey = KeyCode.Q;
    private KeyCode _attackKey = KeyCode.E;
    private bool _isAttackPossible = true;
    private int _attackDamage = 10;
    private int _delay = 1;

    public LayerMask EnemyLayer => _enemyLayer;
    public Transform LeechAttackPoint => _leechAttackPoint;
    public float LeechAttackRange => _leechAttackRange;

    private void Update()
    {
        if (Input.GetKeyDown(_attackKey) && _isAttackPossible)
        {
            Attack();
        }
        else if (Input.GetKeyDown(_leechAttackKey) && _isAttackPossible)
        {
            _leechAttackCoroutine = StartCoroutine(_ability.LeechAttack(_player, _isAttackPossible, this));
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

        StartCoroutine(_cooldown.AttackCooldown(_delay, _isAttackPossible));
    }

    private void OnDrawGizmosSelected()
    {
        if (_leechAttackPoint == null)
            return;

        Gizmos.DrawWireSphere(_leechAttackPoint.position, _leechAttackRange);
    }
}
