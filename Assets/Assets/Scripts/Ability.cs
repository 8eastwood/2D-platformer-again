using System;
using UnityEngine;
using System.Collections;

public class Ability : MonoBehaviour
{
    [SerializeField] private Transform _leechAttackPoint;
    [SerializeField] private Health _health;
    [SerializeField] private float _leechAttackRange;

    private WaitForSeconds _leechDelay;
    private Coroutine _cooldownCoroutine;
    private float _leechAttackDuration = 6;
    private float _currentTime = 0;
    private bool _isAttackPossible = true;
    private int _leechAttackDamage = 7;
    private int _leechDelayTime = 4;

    public event Action<float> Leeched;
    public event Action<Coroutine> Cooldown;
    
    public Coroutine CooldownCoroutine => _cooldownCoroutine;
    public float LeechAttackDuration => _leechAttackDuration;
    public float LeechDelay => _leechDelayTime;
    public float CurrentTime => _currentTime;

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

        while (_currentTime < _leechAttackDuration)
        {
            Collider2D[] hitEnemies = new Collider2D[arraySize];
            int countColliders = Physics2D.OverlapCircleNonAlloc(_leechAttackPoint.position, _leechAttackRange, hitEnemies, enemyLayer);
            Collider2D collider = hitEnemies[0];

            if (collider != null && collider.TryGetComponent(out Enemy enemy))
            {
                _isAttackPossible = false;
                int enemyHealthBeforeAttack = enemy.Health.CurrentAmount;
                enemy.TakeDamage(_leechAttackDamage);
                _health.TakeHeal(enemyHealthBeforeAttack - enemy.Health.CurrentAmount);
            }

            _currentTime++;
            Leeched?.Invoke(_currentTime);
            Debug.Log(_currentTime);

            yield return waitForSecond;
        }

        if (_currentTime != 0)
        {
            _currentTime = 0;
            _cooldownCoroutine = StartCoroutine(Timer.Cooldown(_leechDelay, () => _isAttackPossible = true));
            Cooldown?.Invoke(_cooldownCoroutine);
        }
    }

    private void LeechAttackVisualScale()
    {
        _leechAttackPoint.transform.localScale = new Vector3(_leechAttackRange, _leechAttackRange, _leechAttackRange);
    }
}
