using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Data))]
[RequireComponent(typeof(Tapping))]
public class SpeedWorkers : MonoBehaviour
{
    [SerializeField] private ValueHandler _valueHandler;
    [SerializeField] private TMP_Text _textMoney;
    [SerializeField] private TMP_Text _textLevel;
    [SerializeField] private MoneyTransfer _moneyTransfer;
    [SerializeField] private Image _imageButtonColor;

    //private IntegrationMetric _integrationMetric = new IntegrationMetric();
    private List<Worker> _workers = new List<Worker>();

    private Tapping _tapping;
    private Data _data;
    private float _money = 400;
    private int _level = 0;
    private int _amount = 0;
    private int _count = 0;
    //private string _type = "improvement";
    //private string _name = "AddSpeedWorker";

    private const string MoneySpeed = "MoneySpeed";
    private const string SpeedLevel = "SpeedLevel";
    private const string SaveNameAmount = "AmountMoneySpeedWorker";
    private const string SaveNameCount = "NumberUsesSpeedWorker";

    public Image _ImageButtonColor => _imageButtonColor;
    public float Money => _money;

    private void Start()
    {
        _data = GetComponent<Data>();
        _tapping = GetComponent<Tapping>();
        _money = _data.GetSaveFloat(MoneySpeed, _money);
        _level = _data.GetSave(SpeedLevel, _level);
        _amount = _data.GetSave(SaveNameAmount, _amount);
        _count = _data.GetSave(SaveNameCount, _count);
        _textLevel.text = "lvl." + _level.ToString();
        _textMoney = _moneyTransfer.CurrencyConversion(_money, _textMoney);
    }

    public void RaiseSpeed()
    {
        if(_valueHandler.Money >= _money)
        {
            _amount += (int)_money;
            _count++;
            //_integrationMetric.OnSoftCurrencySpend(_type, _name, _amount, _count,(int)_money);
            PlayerPrefs.SetInt(SaveNameAmount, _amount);
            PlayerPrefs.SetInt(SaveNameCount, _count);
            _valueHandler.PayPurchase(_money);
            _level++;
            PlayerPrefs.SetInt(SpeedLevel, _level);
            _tapping.EnableTapping();
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
        
        _money += 800;
        PlayerPrefs.SetFloat(MoneySpeed, _money);
        _textMoney = _moneyTransfer.CurrencyConversion(_money, _textMoney);
        _textLevel.text = "lvl." + _level.ToString();
    }
}
