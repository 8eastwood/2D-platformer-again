using UnityEngine;

public class HealthView : MonoBehaviour
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

    protected virtual void UpdateHealth()
    {
    }
}
