using System;
using UnityEngine;

public static class Bank
{
    #region Event

    public static event Action<int> OnMoneyChanger;

    #endregion

    private static int _coins;
    private static int Coins
    {
        get { return _coins; }
        set
        {
            _coins = Mathf.Clamp(value, 0, int.MaxValue);
        }
    }

    #region Public Static Method
    public static void AddCoins(int value)
    {
        OnMoneyChanger?.Invoke(value);
        Coins += value;
    }

    public static void SpendCoins(int value)
    {
        Coins -= value;
    }
    
    public static int GetCountCoins()
    {
        return Coins;
    }
    #endregion
}
