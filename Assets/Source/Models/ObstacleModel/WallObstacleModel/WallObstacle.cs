﻿using UnityEngine;

public class WallObstacle : IObstacle
{
    private DamagableView _view;
    private WallObstacleModel _model;
    
    public WallObstacle(DamagableView view, WallObstacleModel model)
    {
        _view = view;
        _model = model;
    }

    public void Init()
    {
        _view.ReceivingDamage += _model.OnReceivingDamage;
        _model.ObstacleBroke += _view.OnViewBroke;
    }
    
    public void Dispose()
    {
        _view.ReceivingDamage -= _model.OnReceivingDamage;
        _model.ObstacleBroke -= _view.OnViewBroke;
    }
}