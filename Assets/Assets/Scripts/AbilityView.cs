using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AbilityView : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Slider _abilitySlider;
    [SerializeField] private Ability _ability;

    private Coroutine _updateSliderCoroutine;
    private Coroutine _rechargeCoroutine;
    private float _currentVelocity = 0.5f;
    private float _amountOfSeconds = 1;
    private float _cooldownStep = 0.2f;
    private float _minValue = 0;
    private float _maxValue = 1;
    private float _delay = 1f;
    private float _step;

    private void Awake()
    {
        _abilitySlider.minValue = _minValue;
        _abilitySlider.maxValue = _maxValue;
        _abilitySlider.value = _maxValue;
        _step = _delay / _ability.LeechAttackDuration;
    }

    private void OnEnable()
    {
        _ability.Leeched += UpdateAbility;
        _ability.Cooldown += UpdateRecharge;
    }

    private void OnDisable()
    {
        _ability.Leeched -= UpdateAbility;
        _ability.Cooldown -= UpdateRecharge;
    }

    private void UpdateAbility()
    {
        if (_updateSliderCoroutine != null)
        {
            StopCoroutine(_updateSliderCoroutine);
        }

        _updateSliderCoroutine = StartCoroutine(UpdateSlider());
    }

    private void UpdateRecharge()
    {
        if (_rechargeCoroutine != null)
        {
            StopCoroutine(_rechargeCoroutine);
        }

        _rechargeCoroutine = StartCoroutine(Recharge());
    }

    private IEnumerator UpdateSlider()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (_ability.CurrentTime < _ability.LeechAttackDuration)
        {
            _abilitySlider.value =
                Mathf.MoveTowards(_abilitySlider.value, _abilitySlider.minValue, _step);
           
            yield return wait;
        }
    }

    private IEnumerator Recharge()
    {
        WaitForSeconds wait = new WaitForSeconds(_amountOfSeconds);
        float rechargeTime = 0;

        while (rechargeTime < _ability.LeechDelay)
        {
            _abilitySlider.value = Mathf.MoveTowards(_abilitySlider.value, _ability.LeechAttackDuration, 1 / 4); // _cooldownStep);
            rechargeTime++;

            yield return wait;
        }
    }
}
