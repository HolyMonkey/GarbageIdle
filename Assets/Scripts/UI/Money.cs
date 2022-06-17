using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    [SerializeField] private ValueHandler _valueHandler;
    [SerializeField] private TMP_Text _money;
    [SerializeField] private MoneyTransfer _moneyTransfer;

    private void OnEnable()
    {
        _valueHandler.MoneyChanged += OnMoneyChanged;
    }

    private void OnDisable()
    {
        _valueHandler.MoneyChanged -= OnMoneyChanged;
    }

    private void Start()
    {
       _money = _moneyTransfer.CurrencyConversion(_valueHandler.Money,_money);
    }

    private void OnMoneyChanged(float money)
    {
        _money = _moneyTransfer.CurrencyConversion(money, _money);
    }
}
