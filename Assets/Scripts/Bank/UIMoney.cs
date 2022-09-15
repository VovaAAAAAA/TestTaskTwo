using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIMoney : MonoBehaviour
{
    private Text _moneyCount;
    private Animator _animator;

    #region Mono
    private void Awake()
    {
        _moneyCount = GetComponentInChildren<Text>();
        _animator = _moneyCount.gameObject.GetComponent<Animator>();
    }
    #endregion

    #region Public Method
    public void MoneyOnDisplay(int amountMoney)
    {
        StartCoroutine(MoneyCorutina(amountMoney));
    }

    public IEnumerator MoneyCorutina(int amountMoney)
    {
        float _currentCountCoins = Bank.GetCountCoins();
        float _coinsAfter = _currentCountCoins + amountMoney;
        _animator.SetBool("AddCoins", true);

        while (_currentCountCoins != _coinsAfter)
        {
            _currentCountCoins = Mathf.MoveTowards(_currentCountCoins, _coinsAfter, 1);
            _moneyCount.text = _currentCountCoins.ToString();
            yield return new WaitForSeconds(0.01f);
        }

        _animator.SetBool("AddCoins", false);
        StopCoroutine(nameof(MoneyCorutina));
    }
    #endregion
}
