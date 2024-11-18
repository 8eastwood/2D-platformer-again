using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxAmount;

    private int _minAmount = 0;

    public int CurrentAmount { get; private set; }

    public Action Death;

    private void Awake()
    {
        CurrentAmount = _maxAmount;
    }

    public void TakeHeal(int healPoints)
    {
        CurrentAmount += healPoints;
        Debug.Log("вылечено " + healPoints + " хп");
    }

    public void TakeDamage(int damage)
    {
        CurrentAmount = Mathf.Clamp(CurrentAmount - damage, _minAmount, _maxAmount);

        if (CurrentAmount <= 0)
        {
            CurrentAmount = 0;
            Death?.Invoke();
            //Die();
        }
    }
}
