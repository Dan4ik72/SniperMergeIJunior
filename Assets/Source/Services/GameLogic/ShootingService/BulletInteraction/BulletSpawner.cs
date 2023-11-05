using System;
using UnityEngine;
using VContainer;

internal class BulletSpawner
{
    private Magazine _magazine;

    private BulletInfo _currentSpawningBullet;

    private float _spawnTime = 1.1f;
    private float _timer;

    public event Action<BulletInfo> BulletSpawned;

    [Inject]
    internal BulletSpawner(Magazine magazine)
    {
        _magazine = magazine;
    }

    public void Init(float spawnTime) => _spawnTime = spawnTime;
    
    public void Update()
    {
        if(_currentSpawningBullet == null)
            return;


        _timer += Time.deltaTime;

        if(_timer < _spawnTime)
            return;
    
        _timer = 0f;
        FillMagazine(_currentSpawningBullet);
    }

    public void ChangeBullet(BulletInfo newBullet) => _currentSpawningBullet = newBullet;

    private void FillMagazine(BulletInfo fillingBullet)
    {
        _magazine.ReceiveBullet(fillingBullet);

        BulletSpawned?.Invoke(fillingBullet);
    }
}