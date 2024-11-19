using UnityEngine;

[RequireComponent (typeof(Health))]
public class Death : MonoBehaviour
{
    [SerializeField] private Health _health;

    private void OnEnable()
    {
        _health.Death += Die;
    }

    private void Die()
    {
        enabled = false;
        Destroy(gameObject);
    }
}
