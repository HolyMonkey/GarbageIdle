using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Data))]
public class ValueHandler : MonoBehaviour
{
    private Data _data;
    private float _money = 5000;

    private IntegrationMetric _integrationMetric = new IntegrationMetric();
    public float Money => _money;

    public event Action<float> MoneyChanged;

    private const string Balance = "Money";

    private void Start()
    {
        _data = GetComponent<Data>();
        _money = _data.GetSaveFloat(Balance, _money);
        _integrationMetric.OnCurrentSoftBalance((int)_money);
        MoneyChanged?.Invoke(_money);
    }

    public void AddMoney(Worker worker)
    {
        _money += worker.Target.Price;
        MoneyChanged?.Invoke(_money);
        PlayerPrefs.SetFloat("Money", _money);
    }

    public void PayPurchase(float money)
    {
        _money -= money;
        MoneyChanged?.Invoke(_money);
        PlayerPrefs.SetFloat("Money", _money);
    }

    public void AddReward(float reward)
    {
        _money += reward;
        MoneyChanged?.Invoke(_money);
        PlayerPrefs.SetFloat("Money", _money);
    }
}
