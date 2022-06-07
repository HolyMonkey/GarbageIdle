using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueHandler : MonoBehaviour
{
    private float _money = 5000;

    public event Action<float> MoneyChanged;

    public float Money => _money;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Money"))
            _money = PlayerPrefs.GetFloat("Money");

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
