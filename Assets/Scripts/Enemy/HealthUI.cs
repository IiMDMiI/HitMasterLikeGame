using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Slider))]
public class HealthUI : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Slider _slider;

    private void Awake() => _enemy.OnHealthChanged += OnHealthChangedHandler;
    private void OnDestroy() => _enemy.OnHealthChanged -= OnHealthChangedHandler;
    private void OnHealthChangedHandler(float health) => _slider.value = health / _enemy.MaxHealth;

}
