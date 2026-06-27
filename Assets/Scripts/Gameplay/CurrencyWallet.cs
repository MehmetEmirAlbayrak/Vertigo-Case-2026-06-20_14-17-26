using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyWallet : MonoBehaviour
{
    int current;

    public event Action OnChanged;

    public int Current => current;

    private readonly string currencyKey = "Currency";


    [SerializeField] private int startingAmount = 0;

    private void Awake()
    {
        if(startingAmount == 0)
            current = PlayerPrefs.GetInt(currencyKey, startingAmount);
        else
            current = startingAmount;
    }

    public bool TrySpend(int amount)
    {
        if (current >= amount)
        {
            current -= amount;
            Save();
            OnChanged?.Invoke();
            return true;
        }
        return false;
    }

    public void Add(int amount)
    {
        current += amount;
        Save();
        OnChanged?.Invoke();
    }

    private void Save()
    {
        PlayerPrefs.SetInt(currencyKey, current);
        PlayerPrefs.Save();
    }

}
