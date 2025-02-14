using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action DamageTaken;
    public event Action Healed;
    public event Action Died;

    public int CurrentAmount { get; private set; }
    public int MinAmount { get; private set; } = 0;
    public int MaxAmount { get; private set; } = 100;

    private void Awake()
    {
        CurrentAmount = MaxAmount;
    }

    public void TakeHeal(int healPoints)
    {
        CurrentAmount = Math.Clamp(CurrentAmount + healPoints, MinAmount, MaxAmount);
        Healed?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        CurrentAmount = Mathf.Clamp(CurrentAmount - damage, MinAmount, MaxAmount);
        DamageTaken?.Invoke();

        if (CurrentAmount <= 0)
        {
            CurrentAmount = 0;
            Died?.Invoke();
        }
    }
}
