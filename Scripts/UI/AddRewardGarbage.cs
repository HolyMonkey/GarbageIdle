using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddRewardGarbage : MonoBehaviour
{
    [SerializeField] private ValueHandler _valueHandler;
    [SerializeField] private TMP_Text _textMoney;
    [SerializeField] private TMP_Text _textLevel;
    [SerializeField] private MoneyTransfer _moneyTransfer;

    private float _money = 600;
    private int _level = 0;


    private List<Garbage> _garbage = new List<Garbage>();

    private void Start()
    {
        if (PlayerPrefs.HasKey("MoneyRewardGarbage") && PlayerPrefs.HasKey("RewardLevelWorker"))
        {
            _money = PlayerPrefs.GetFloat("MoneyRewardGarbage");
            _level = PlayerPrefs.GetInt("RewardLevelWorker");
        }

        _textMoney = _moneyTransfer.CurrencyConversion(_money, _textMoney);
        _textLevel.text = "lvl." + _level.ToString();
    }

    public void AddListGarbage(Garbage garbage)
    {
        _garbage.Add(garbage);
    }

    public void AssignValue()
    {
        if (_valueHandler.Money >= _money)
        {
            _valueHandler.PayPurchase(_money);
            _level++;
            PlayerPrefs.SetInt("RewardLevelWorker", _level);

            foreach (var item in _garbage)
            {
                if (item.Removed == false)
                {
                    item.RisePrice();
                }
            }
            SetPrice();
        }
        else
            return;
    }

    private void SetPrice()
    {
        _money += 400;
        PlayerPrefs.SetFloat("MoneyRewardGarbage", _money);
        _textMoney = _moneyTransfer.CurrencyConversion(_money, _textMoney);
        _textLevel.text = "lvl." + _level.ToString();
    }  
}
