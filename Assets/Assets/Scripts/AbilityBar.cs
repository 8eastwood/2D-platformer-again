using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AbilityBar : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Slider _abilitySlider;
    [SerializeField] private Ability _ability;

    private Coroutine _coroutine;
    private float _currentVelocity = 0.5f;
    private float _amountOfSeconds = 1;
    private float _cooldownStep = 0.2f;
    private float _delay = 0.2f;
    private float _step = 0.5f;

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
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(UpdateSlider());
    }

    //private IEnumerator UpdateSlider(float start, float end)
    //{
    //    WaitForSeconds wait = new WaitForSeconds(_delay);
    //    float currentTime = end;

    //    while (start != end)
    //    {
    //        start += Time.deltaTime;
    //        //_abilitySlider.value = Mathf.MoveTowards(_abilitySlider.value, _abilitySlider.minValue, _step);
    //        //_abilitySlider.value = Mathf.SmoothDamp(_abilitySlider.value, _abilitySlider.minValue, ref _currentVelocity, _ability.LeechAttackDuration * Time.deltaTime);
    //        _abilitySlider.value = Mathf.Clamp(currentTime / end, _abilitySlider.minValue, _currentVelocity);
    //        //start++;

    //        Debug.Log("slider coming down" + start);
    //        //написать формулу стремления одного значения к другому без мув товардс. Слайдер должен достигать нуля к моменту завершения работы абилки. 

    //        yield return wait;
    //    }

    //    if (_abilitySlider.value <= _abilitySlider.minValue)
    //    {
    //        StartCoroutine(Recharge());
    //    }
    //}

    private IEnumerator UpdateSlider()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (_ability.CurrentTime != _ability.LeechAttackDuration)
        {
            _abilitySlider.value = Mathf.MoveTowards(_abilitySlider.value, _abilitySlider.minValue, _step );
            Debug.Log("slider now is " + _abilitySlider.value);

            yield return wait;
        }

        if (_abilitySlider.value <= _abilitySlider.minValue)
        {
            StartCoroutine(Recharge());
        }
    }

    private IEnumerator Recharge()
    {
        WaitForSeconds wait = new WaitForSeconds(_amountOfSeconds);
        float rechargeTime = 0;

        while (rechargeTime < _ability.LeechDelay)
        {
            //_abilitySlider.value = Mathf.MoveTowards(_abilitySlider.value, _ability.LeechAttackDuration, _cooldownStep);
            _abilitySlider.value = Mathf.SmoothDamp(_abilitySlider.value, _abilitySlider.maxValue, ref _currentVelocity, _ability.LeechAttackDuration);
            rechargeTime++;
            Debug.Log("slider now is " + _abilitySlider.value);

            yield return wait;
        }

        Debug.Log("stop recharging");
        Debug.Log("slider now is " + _abilitySlider.value);
    }
}
