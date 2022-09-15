using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewMoney : MonoBehaviour
{
    [Header("Text for coins on the display")]
    [SerializeField] private Text _moneyCount;

    [SerializeField] private UIMoney _moneyUI;

    #region Mono
    private void Start()
    {
        _moneyCount.text = Bank.GetCountCoins().ToString();
    }

    private void OnEnable()
    {
        Bank.OnMoneyChanger += _moneyUI.MoneyOnDisplay;
    }

    private void OnDisable()
    {
        Bank.OnMoneyChanger -= _moneyUI.MoneyOnDisplay;
    }
    #endregion
}
