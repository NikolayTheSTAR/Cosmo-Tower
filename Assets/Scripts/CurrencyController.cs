using System;
using System.Collections.Generic;
using UnityEngine;
using TheSTAR.Data;
using TheSTAR.Utility;
using Zenject;

public class CurrencyController : MonoBehaviour
{
    private List<ITransactionReactable> _transactionReactables;

    [Inject] private readonly DataController _data;

    public void Init(List<ITransactionReactable> trs)
    {
        _transactionReactables = trs;
        InitReaction();
    }

    public void AddCurrency(CurrencyType itemType, int count = 1, bool autoSave = true)
    {
        _data.gameData.battleData.AddCurrency(itemType, count, out int result);
        if (autoSave) _data.Save();

        Reaction(itemType, result);
    }

    public void ReduceCurrency(CurrencyType itemType, int count = 1, bool autoSave = true, Action completeAction = null, Action failAction = null)
    {
        if (_data.gameData.battleData.GetCurrencyCount(itemType) >= count)
        {
            _data.gameData.battleData.AddCurrency(itemType, -count, out int result);
            if (autoSave) _data.Save();

            Reaction(itemType, result);

            completeAction?.Invoke();
        }
        else failAction?.Invoke();
    }

    public void ClearCurrency(CurrencyType currencyType)
    {
        var count = GetCurrencyValue(currencyType);
        _data.gameData.battleData.AddCurrency(currencyType, -count, out _);
    }

    public int GetCurrencyValue(CurrencyType currencyType)
    {
        return _data.gameData.battleData.GetCurrencyCount(currencyType);
    }

    private void InitReaction()
    {
        var itemTypes = EnumUtility.GetValues<CurrencyType>();

        foreach (var tr in _transactionReactables)
            foreach (var itemType in itemTypes)
                tr.OnTransactionReact(itemType, _data.gameData.battleData.GetCurrencyCount(itemType));
    }

    private void Reaction(CurrencyType itemType, int finalValue)
    {
        foreach (var tr in _transactionReactables) tr.OnTransactionReact(itemType, finalValue);
    }
}

public interface ITransactionReactable
{
    void OnTransactionReact(CurrencyType itemType, int finalValue);
}

public enum CurrencyType
{
    Coin
}