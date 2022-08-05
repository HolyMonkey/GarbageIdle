using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardedAdView : MonoBehaviour
{

    private TMP_Text _moneyInfoReward;


    private TakeReward _takeReward;
    private MoneyTransfer _moneyTransfer;
    private ValueHandler _valueHandler;


    private void Start()
    {
        
        _moneyInfoReward = GetComponent<TMP_Text>();
        ShowReward();
        //_takeReward.gameObject.SetActive(true);
        //_moneyInfoReward.text = ("+" + _takeReward.RewardInfo/1000).ToString() + " K$";
        //_takeReward.gameObject.SetActive(false);

        
    }

    public void ShowReward()
    {
        _moneyInfoReward = GetComponent<TMP_Text>();
        //_takeReward = FindObjectOfType<TakeReward>();
        _moneyTransfer = FindObjectOfType<MoneyTransfer>();
        _valueHandler = FindObjectOfType<ValueHandler>();
        float money = 200;

        if (money < (_valueHandler.Money / 4))
        {
            money = (float)Math.Round((_valueHandler.Money / 4),0);
        }

        _moneyInfoReward = _moneyTransfer.CurrencyConversion(money, _moneyInfoReward);
    }

}
