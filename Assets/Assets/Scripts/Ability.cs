using System;
using UnityEngine;
using System.Collections;

public class Ability : MonoBehaviour
{
    [SerializeField] private Cooldown _cooldown;

    private float _leechAttackDuration = 6;
    private float _currentTime = 0;
    private int _leechDelay = 4;
    private int _leechAttackDamage = 7;

    public float LeechAttackDuration => _leechAttackDuration;
    public float CurrentTime => _currentTime;
    public float LeechDelay => _leechDelay;

    public event Action Leeched;

    public IEnumerator LeechAttack(Player player, bool isAttackPossible, PlayerCombat playerCombat)
    {
        int arraySize = 1;
        float amountOfSeconds = 1;
        Collider2D[] hitEnemies = new Collider2D[arraySize];
        int countColliders = Physics2D.OverlapCircleNonAlloc(playerCombat.LeechAttackPoint.position, playerCombat.LeechAttackRange, hitEnemies, playerCombat.EnemyLayer);
        Collider2D collider = hitEnemies[0];
        var waitForSecond = new WaitForSeconds(amountOfSeconds);

        while (_currentTime <= _leechAttackDuration && collider != null)
        {
            if (collider.TryGetComponent(out Enemy enemy))
            {
                isAttackPossible = false;
                Leeched?.Invoke();
                int enemyHealthBeforeAttack = enemy.Health.CurrentAmount;
                enemy.TakeDamage(_leechAttackDamage);
                player.Health.TakeHeal(enemyHealthBeforeAttack - enemy.Health.CurrentAmount);
            }

            _currentTime++;

            yield return waitForSecond;
        }

        _currentTime = 0;

        StartCoroutine(_cooldown.AttackCooldown(_leechDelay, isAttackPossible));
    }
}
