using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent (typeof(Health))]
[RequireComponent(typeof(Player))]

public class Player : MonoBehaviour
{
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    public void Heal(int healPoints)
    {
        _health.TakeHeal(healPoints);
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }
}
