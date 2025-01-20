using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent (typeof(Health))]
[RequireComponent(typeof(Player))]
public class Player : MonoBehaviour
{
    [SerializeField] private Health _health;

    public Health Health => _health;

    //private void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.E))
    //    {
    //        TakeDamage(10);
    //    }
    //}

    public void Heal(int healPoints)
    {
        _health.TakeHeal(healPoints);
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }
}
