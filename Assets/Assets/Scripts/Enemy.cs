using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Enemy))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private readonly int Running = Animator.StringToHash(nameof(Running));
    private Health _health;

    public Health Health => _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _animator.SetBool(Running, true);
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }
}
