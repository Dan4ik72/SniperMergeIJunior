﻿using System;
using UnityEngine;
using VContainer;

public class MainMenuBootstrapper : MonoBehaviour, IDisposable
{
    [Inject] private UIBootstrapService _uiBootstrapService;
    [Inject] private PlayerMoneyService _playerMoneyService;
    [Inject] private DataStorageService _dataStorageService;
    [Inject] private LevelLoadService _levelLoadService;
    [Inject] private InterpreterService _interpreterService;

    private void Start()
    {
        _levelLoadService.Init(); 
        _playerMoneyService.Init();
        _uiBootstrapService.Init();
        _interpreterService.SetUpLocalization();
        YandexGameInterstitialHandler.InvokeInterstitial();
    }

    private void Update()
    {
    }
    
    public void Dispose()
    {
        _uiBootstrapService.Disable();
    }
}
