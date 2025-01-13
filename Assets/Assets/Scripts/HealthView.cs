using UnityEngine;

public abstract class HealthView : MonoBehaviour
{
    [SerializeField] protected Health _health;

    private void OnEnable()
    {
        _health.DamageTaken += UpdateHealth;
        _health.Healed += UpdateHealth;
    }

    private void OnDisable()
    {
        _health.DamageTaken -= UpdateHealth;
        _health.Healed -= UpdateHealth;
    }

    protected abstract void UpdateHealth();
}
