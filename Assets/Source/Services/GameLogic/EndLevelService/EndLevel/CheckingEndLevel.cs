using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

internal class CheckingEndLevel
{
    private IReadOnlyList<IDamageble> _enemies;
    private IDamageble _gun;

    private int _totalEnemies;
    private int _counterKilledEnemies;

    public event Action Losed;
    public event Action Victory;

    [Inject]
    public CheckingEndLevel(LevelInfo levelConfig)
    {
        _totalEnemies = levelConfig.AllEnemies;
    }

    public void Init(IReadOnlyList<IDamageble> enemies, IDamageble gun)
    {
        _enemies = enemies;
        _gun = gun;
        _gun.Died += OnDieGun;
        _counterKilledEnemies = 0;

        foreach (var enemy in _enemies)
            enemy.Died += OnDieEnemy;
    }

    public void Disable()
    {
        _gun.Died -= OnDieGun;

        foreach (var enemy in _enemies)
            enemy.Died -= OnDieEnemy;

        _counterKilledEnemies = 0;
    }

    private void OnDieEnemy(IDamageble damageble)
    {
        _counterKilledEnemies++;

        if (_counterKilledEnemies == _totalEnemies)
            Victory?.Invoke();
    }

    private void OnDieGun(IDamageble damageble) => Losed?.Invoke();
}