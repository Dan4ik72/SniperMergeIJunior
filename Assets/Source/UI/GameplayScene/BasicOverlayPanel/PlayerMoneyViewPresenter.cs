﻿using VContainer;

public class PlayerMoneyViewPresenter
{
    private UpdatableTextView _view;
    private LevelWalletService _model;

    public PlayerMoneyViewPresenter(UpdatableTextView view, LevelWalletService model)
    {
        _view = view;
        _model = model;
    }
    
    public void Init()
    {
        _view.Init(_model.GetCurrentMoneyCount().ToString());
        
        _model.MoneyReceived += UpdateViewText;
        _model.MoneySpent += UpdateViewText;
    }

    public void Disable()
    {
        _model.MoneyReceived -= UpdateViewText;
        _model.MoneySpent -= UpdateViewText;
    }

    private void UpdateViewText(uint newValue)
    {
        _view.UpdateText(newValue.ToString());
    }
}