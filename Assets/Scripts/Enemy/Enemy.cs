using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100;
    private float _currentHealth;
    public Action<float> OnHealthChanged;
    
    public float MaxHealth => _maxHealth;
    public float CurrentHealth
    {   
        get => _currentHealth;
        private set
        {
            _currentHealth = value;
            OnHealthChanged?.Invoke(_currentHealth);
        }
    }
    public event Action<Enemy> OnEnemyDied;
    private void Start() => CurrentHealth = _maxHealth;

    public void GetDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
            OnEnemyDied?.Invoke(this);
    }



}
