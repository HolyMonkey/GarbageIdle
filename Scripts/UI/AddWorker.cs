using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddWorker : MonoBehaviour
{
    [SerializeField] private ValueHandler _valueHandler;
    [SerializeField] private TMP_Text _textMoney;
    [SerializeField] private TMP_Text _textLevel;
    [SerializeField] private SpawnerWorker _spawner;
    [SerializeField] private MoneyTransfer _moneyTransfer;

    private float _money = 500;
    private int _level = 0;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MoneyAddWorker") && PlayerPrefs.HasKey("CountAddWorker"))
        {
            _money = PlayerPrefs.GetFloat("MoneyAddWorker");
            _level = PlayerPrefs.GetInt("CountAddWorker");
            _spawner.AddCount(_level);
        }
        _textMoney = _moneyTransfer.CurrencyConversion(_money, _textMoney);
        _textLevel.text = "lvl." + _level.ToString();
    }


    public void CreateEmployee()
    {
        if (_valueHandler.Money >= _money)
        {
            _valueHandler.PayPurchase(_money);
            _spawner.Initilaze();
            _level++;
            PlayerPrefs.SetInt("CountAddWorker", _level);
            SetPrice();
        }
        else
            return;
    }

    private void SetPrice()
    {
        _money += 500;
        _textLevel.text = "lvl." + _level;
        _textMoney = _moneyTransfer.CurrencyConversion(_money, _textMoney);
        PlayerPrefs.SetFloat("MoneyAddWorker", _money);
    }
}
