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
    private float _amountOfSeconds = 1f;
    private float _minValue = 0;
    private float _maxValue = 1;
    private float _step = 0.2f;
    private WaitForSeconds _wait;

    private void Awake()
    {
        _abilitySlider.minValue = _minValue;
        _abilitySlider.maxValue = _maxValue;
        _abilitySlider.value = _maxValue;
        _wait = new WaitForSeconds(_amountOfSeconds);
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

    private void UpdateAbility(float currentTime)
    {
        if (_updateSliderCoroutine != null)
        {
            StopCoroutine(_updateSliderCoroutine);
        }

        _updateSliderCoroutine = StartCoroutine(UpdateSlider(currentTime));
    }

    private void UpdateRecharge(Coroutine coroutine)
    {
        if (_rechargeCoroutine != null)
        {
            StopCoroutine(_rechargeCoroutine);
        }

        _rechargeCoroutine = StartCoroutine(Recharge(coroutine));
    }

    private float SliderTargetDestination(float currentTime)
    {
        float value = _abilitySlider.maxValue - currentTime / _ability.LeechAttackDuration;

        return value;
    }
    
    private IEnumerator UpdateSlider(float currentTime)
    {
        while (_abilitySlider.value > SliderTargetDestination(currentTime))
        {
            _abilitySlider.value = Mathf.MoveTowards(_abilitySlider.value,
                SliderTargetDestination(currentTime), _step);

            yield return _wait;
        }
    }

    private IEnumerator Recharge(Coroutine coroutine)
    {
        float rechargeTime = 0;

        while (coroutine != null && _abilitySlider.value < _abilitySlider.maxValue)
        {
            _abilitySlider.value = Mathf.MoveTowards(_abilitySlider.value,
                _abilitySlider.minValue + rechargeTime / _ability.LeechCooldownDuration, _step);
            rechargeTime++;

            yield return _wait;
        }
    }
}