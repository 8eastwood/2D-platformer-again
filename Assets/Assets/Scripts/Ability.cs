using System;
using UnityEngine;
using System.Collections;

public class Ability : MonoBehaviour
{
    [SerializeField] private Transform _leechAttackPoint;
    [SerializeField] private float _leechAttackRange;
    [SerializeField] private Health _health;

    private WaitForSeconds _leechDelay;
    private float _leechAttackDuration = 6;
    private float _currentTime = 0;
    private int _leechAttackDamage = 7;
    private int _leechDelayTime = 4;
    private bool _isAttackPossible = true;

    public event Action Leeched;
    public event Action Cooldown;

    public float LeechAttackDuration => _leechAttackDuration;
    public float CurrentTime => _currentTime;
    public float LeechDelay => _leechDelayTime;

    private void Awake()
    {
        _leechDelay = new WaitForSeconds(_leechDelayTime);

        LeechAttackVisualScale();
    }

    public void TryStart(LayerMask enemyLayer)
    {
        if (_isAttackPossible)
        {
            StartCoroutine(LeechAttack(enemyLayer));
        }
    }

    private IEnumerator LeechAttack(LayerMask enemyLayer)
    {
        float amountOfSeconds = 1;
        int arraySize = 1;
        var waitForSecond = new WaitForSeconds(amountOfSeconds);
        Debug.Log("Leech attack is active");

        while (_currentTime < _leechAttackDuration)
        {
            Collider2D[] hitEnemies = new Collider2D[arraySize];
            int countColliders = Physics2D.OverlapCircleNonAlloc(_leechAttackPoint.position, _leechAttackRange, hitEnemies, enemyLayer);
            Collider2D collider = hitEnemies[0];
            Leeched?.Invoke();

            if (collider != null && collider.TryGetComponent(out Enemy enemy))
            {
                Debug.Log("Enemy founded");
                _isAttackPossible = false;
                int enemyHealthBeforeAttack = enemy.Health.CurrentAmount;
                enemy.TakeDamage(_leechAttackDamage);
                _health.TakeHeal(enemyHealthBeforeAttack - enemy.Health.CurrentAmount);
                Debug.Log("Enemy leeched");
            }

            _currentTime++;
            Debug.Log(_currentTime);

            yield return waitForSecond;
        }

        if (_currentTime != 0)
        {
            Debug.Log("currentTime = attack duration");
            _currentTime = 0;
            Cooldown?.Invoke();
            StartCoroutine(Timer.Cooldown(_leechDelay, () => _isAttackPossible = true));
        }
    }

    private void LeechAttackVisualScale()
    {
        _leechAttackPoint.transform.localScale = new Vector3(_leechAttackRange, _leechAttackRange, _leechAttackRange);
    }
}
