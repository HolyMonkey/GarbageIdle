using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Data))]
public class AddWorker : MonoBehaviour
{
    [SerializeField] private ValueHandler _valueHandler;
    [SerializeField] private TMP_Text _textMoney;
    [SerializeField] private TMP_Text _textLevel;
    [SerializeField] private SpawnerWorker _spawner;
    [SerializeField] private MoneyTransfer _moneyTransfer;

    private Data _data;
    private IntegrationMetric _integrationMetric = new IntegrationMetric();

    private float _money = 500;
    private int _level = 0;
    private string _type = "improvement";
    private string _name = "AddWorker";
    private int _amount = 0;
    private int _count = 0;

    private const string MoneyAddWorker = "MoneyAddWorker";
    private const string LevelAddWorker = "LevelAddWorker";
    private const string SaveNameAmount = "AmountMoneyWorker";
    private const string SaveNameCount = "NumberUsesAddWorker";

    private void Start()
    {
        _data = GetComponent<Data>();
        _money = _data.GetSaveFloat(MoneyAddWorker, _money);
        _level = _data.GetSave(LevelAddWorker, _level);
        _spawner.AddCount(_level);
        _textMoney = _moneyTransfer.CurrencyConversion(_money, _textMoney);
        _textLevel.text = "lvl." + _level.ToString();
    }


    public void CreateEmployee()
    {
        if (_valueHandler.Money >= _money)
        {
            _amount += (int)_money;
            _count++;
            _integrationMetric.OnSoftCurrencySpend(_type, _name, _amount, _count,(int)_money);
            PlayerPrefs.SetInt(SaveNameAmount, _amount);
            PlayerPrefs.SetInt(SaveNameCount, _count);
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
