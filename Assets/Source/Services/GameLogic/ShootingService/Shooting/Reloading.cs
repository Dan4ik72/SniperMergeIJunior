using System;
using System.Collections.Generic;
using VContainer;

internal class Reloading : IBuffable
{
    private GunData _data;
    private float _currentSpeed;
    private float _elapsedTime = 0;
    private bool _isCompleted = false;

    private List<ShootingSpeedBuff> _currentBuffs = new();
    
    public Action Ready;

    internal void Init(GunData data)
    {
        _data = data;
        _currentSpeed = _data.SpeedCooldown;
    }

    public Type BuffableType => typeof(ShootingSpeedBuff);

    public bool IsLoaded => _isCompleted;

    public void Update(float delta)
    {
        if (_isCompleted)
            return;

        _elapsedTime += delta;

        if (_elapsedTime > _currentSpeed)
            _isCompleted = true;
    }

    public void Reduce(float value)
    {
        float newTime = _currentSpeed -= value;
        _currentSpeed = _currentSpeed > newTime ? newTime : _currentSpeed;
        _currentSpeed = _currentSpeed < _data.MinSpeedCooldown ? _data.MinSpeedCooldown : _currentSpeed;
    }

    public void Reset()
    {
        _elapsedTime = 0;
        _currentSpeed = _data.SpeedCooldown;
        _isCompleted = false;
    }

    public void ApplyBuff(Buff Buff)
    {
        var speedBuff = TryToCastType(Buff);
        
        _currentSpeed *= speedBuff.SpeedMultiplier;
        _currentBuffs.Add(speedBuff);
    }

    public void EndBuff(Buff Buff)
    {
        var speedBuff = TryToCastType(Buff);
        
        if(_currentBuffs.Contains(speedBuff) == false)
            return;

        _currentSpeed /= speedBuff.SpeedMultiplier;
        _currentBuffs.Remove(speedBuff);
    }

    public bool CheckType(Buff type)
    {
        return typeof(ShootingSpeedBuff) == type.GetType();
    }
    
    private ShootingSpeedBuff TryToCastType(Buff Buff)
    {
        if (Buff.GetType() != BuffableType)
            throw new InvalidCastException("Invalid type boxed in the passed argument");

        return (ShootingSpeedBuff)Buff;
    }
}
