using UnityEngine;
using VContainer;

internal class BulletConveyor
{
    private BulletViewFactory _bulletViewFactory;
    private ObjectPool<BulletView> _bulletViewPool = new();

    private BulletConveyorMover _mover;
    private BulletViewsHolder _bulletViewsHolder;

    private int _bulletViewPoolCapacity = 30;
    
    private BulletInfo _currentBulletInfo;
    private BulletInfo _previousBulletInfo;

    private Transform _bulletViewSpawnPosition;

    [Inject]
    internal BulletConveyor(BulletViewFactory bulletViewFactory, BulletConveyorMover mover, BulletViewsHolder holder, Transform bulletViewSpawnPosition)
    {
        _bulletViewFactory = bulletViewFactory;
        _mover = mover;
        _bulletViewsHolder = holder;
        _bulletViewSpawnPosition = bulletViewSpawnPosition;
    }

    public void Init()
    {
        _mover.Arrived += OnBulletArrived;
    }

    public void SetNewBulletInfo(BulletInfo newBulletInfo)
    {
        if (_currentBulletInfo != null)
            _previousBulletInfo = _currentBulletInfo;

        _currentBulletInfo = newBulletInfo;
        FillPoll();
    }

    public void SpawnBulletView(BulletInfo bulletInfo)
    {
        if(_currentBulletInfo == null)
            return;

        if (_bulletViewPool.TryGetAvailableObject(out BulletView obj, _currentBulletInfo.BulletViewPrefab.Level) == false)
        {
            Debug.LogWarning("There is no available bulletView in the object pool");
            return;
        }

        obj.SetAlive(true);
        obj.transform.position = _bulletViewSpawnPosition.transform.position;
        _mover.Move(obj);
    }

    private void OnBulletArrived(BulletView view)
    {
        _bulletViewsHolder.PlaceViewToGrid(view);
    }

    public void OnBulletUsed(BulletInfo bulletInfo)
    {
        var removing = _bulletViewsHolder.RemoveView();

        if (removing == null)
            return;

        _bulletViewPool.ReturnToPool(removing);
        removing.SetAlive(false);
    }

    public void Disable()
    {
        _mover.Arrived -= OnBulletArrived;
    }

    private void FillPoll()
    {    
        if(_previousBulletInfo != null && _currentBulletInfo.Type == _previousBulletInfo.Type)
            return;

        for (int i = 0; i < _bulletViewPoolCapacity; i++)
        {
            var created = _bulletViewFactory.CreateBulletView(_currentBulletInfo);
            _bulletViewPool.AddObject(created, false);
        }
    }
}