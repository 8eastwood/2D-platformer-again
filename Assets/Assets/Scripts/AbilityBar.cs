using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AbilityBar : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Slider _abilitySlider;
    [SerializeField] private Ability _ability;

    private Coroutine _coroutine;
    private float _amountOfSeconds = 1;
    private float _cooldownStep = 2f;
    private float _delay = 0.2f;
    private float _step = 0.2f;

    private void Awake()
    {
        _abilitySlider.minValue = 0;
        _abilitySlider.maxValue = _ability.LeechAttackDuration;
        _abilitySlider.value = _ability.LeechAttackDuration;
    }

    private void OnEnable()
    {
        _ability.Leeched += UpdateAbility;
    }

    private void OnDisable()
    {
        _ability.Leeched -= UpdateAbility;
    }

    private void UpdateAbility()
    {
        _coroutine = StartCoroutine(UpdateSlider());
    }

    private IEnumerator UpdateSlider()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (_ability.CurrentTime != _ability.LeechAttackDuration)
        {
            _abilitySlider.value = Mathf.MoveTowards(_abilitySlider.value, _abilitySlider.minValue, _step);

            yield return wait;
        }

        if (_abilitySlider.value <= _abilitySlider.minValue)
        {
            StartCoroutine(UpdateCooldown());
        }
    }

    private IEnumerator UpdateCooldown()
    {
        WaitForSeconds wait = new WaitForSeconds(_amountOfSeconds);
        float rechargeTime = 0;

        while (rechargeTime < _ability.LeechDelay)
        {
            _abilitySlider.value = Mathf.MoveTowards(_abilitySlider.value, _ability.LeechAttackDuration, _cooldownStep);
            rechargeTime++;

            yield return wait;
        }
    }
}
