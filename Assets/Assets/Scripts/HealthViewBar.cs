using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthViewBar : HealthView
{
    [SerializeField] private Transform _target;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Vector3 _offset;

    private Coroutine _coroutine;
    private float _step = 2f;
    private float _delay = 0.2f;

    private void Awake()
    {
        _healthSlider.minValue = _health.MinAmount;
        _healthSlider.maxValue = _health.MaxAmount;
        _healthSlider.value = _health.MaxAmount;
        transform.position = _target.position + _offset;
    }

    protected override void UpdateHealth()
    {
        _coroutine = StartCoroutine(UpdateSlider());
    }

    private IEnumerator UpdateSlider()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (_healthSlider.value != _health.CurrentAmount)
        {
            _healthSlider.value = Mathf.MoveTowards(_healthSlider.value, _health.CurrentAmount, _step);

            yield return wait;
        }

        //if (_healthSlider.value == _health.CurrentAmount && _coroutine != null)
        //{
        //    StopCoroutine(_coroutine);
        //}
    }
}
