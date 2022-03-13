using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleArea : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;
    public event Action<BattleArea> OnAreaCleared;
    public bool IsCleared
    {
        get
        {
            if (_enemies == null || _enemies.Count == 0)
                return true;
            else
                return false;
        }
    }

    private void Start()
    {
        if (_enemies == null)
            return;
        foreach (var enemy in _enemies)
            enemy.OnEnemyDied += OnEnemyDiedHandler;
    }
    private void OnEnemyDiedHandler(Enemy enemy)
    {
        enemy.OnEnemyDied -= OnEnemyDiedHandler;
        _enemies.Remove(enemy);
        if (_enemies.Count == 0)
            OnAreaCleared?.Invoke(this);
    }


}
