using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedWorkers : MonoBehaviour
{
    [SerializeField] private ValueHandler _valueHandler;
    [SerializeField] private TMP_Text _textMoney;
    [SerializeField] private TMP_Text _textLevel;
    [SerializeField] private MoneyTransfer _moneyTransfer;

    private int _money = 200;
    private int _level = 0;

    private List<Worker> _workers = new List<Worker>();

    private void Start()
    {
        if (PlayerPrefs.HasKey("MoneySpeed")&& PlayerPrefs.HasKey("SpeedLevel"))
        {
            _money = PlayerPrefs.GetInt("MoneySpeed");
            _level = PlayerPrefs.GetInt("SpeedLevel");
        }
        _textLevel.text = "lvl." + _level.ToString();
        _textMoney = _moneyTransfer.CurrencyConversion(_money, _textMoney);
    }

    public void RaiseSpeed()
    {
        if(_valueHandler.Money >= _money)
        {
            _valueHandler.PayPurchase(_money);
            _level++;
            PlayerPrefs.SetInt("SpeedLevel", _level);
            SetPrice();

            foreach (var item in _workers)
            {
                item.SettingSpeed();
            }
        }
    }

    public void AddListGarbage(Worker worker)
    {
        _workers.Add(worker);
    }

    private void SetPrice()
    {
        
        _money += 200;
        PlayerPrefs.SetInt("MoneySpeed", _money);
        _textMoney = _moneyTransfer.CurrencyConversion(_money, _textMoney);
        _textLevel.text = "lvl." + _level.ToString();
    }
}
