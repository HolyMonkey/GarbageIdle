using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyTransfer : MonoBehaviour
{
    [SerializeField] private AddRewardGarbage _addRewardGarbage;
    [SerializeField] private AddWorker _addWorker;
    [SerializeField] private SpeedWorkers _speedWorkers;
    [SerializeField] private ValueHandler _valueHandler;

    public event Action<float, float, Image> GarbageColorChangedButton;
    public event Action<float, float, Image> WorkerColorChangedButton;
    public event Action<float, float, Image> SeedWorkerColorChangedButton;

    private void OnEnable()
    {
        GarbageColorChangedButton += PurchaseAvailable;
        WorkerColorChangedButton += PurchaseAvailable;
        SeedWorkerColorChangedButton += PurchaseAvailable;
    }

    private void OnDisable()
    {
        GarbageColorChangedButton -= PurchaseAvailable;
        WorkerColorChangedButton -= PurchaseAvailable;
        SeedWorkerColorChangedButton -= PurchaseAvailable;
    }

    private void Start()
    {
        GarbageColorChangedButton?.Invoke(_valueHandler.Money, _addRewardGarbage.Money, _addRewardGarbage.ImageButton);
        WorkerColorChangedButton?.Invoke(_valueHandler.Money, _addWorker.Money, _addWorker.ImageButtonColor);
        SeedWorkerColorChangedButton?.Invoke(_valueHandler.Money, _speedWorkers.Money, _speedWorkers._ImageButtonColor);
    }

    public void PurchaseAvailable(float money, float moneyButton, Image image)
    {
        Vector4 color;
        if (money < moneyButton)
        {
            color = new Vector4(127 / 255.0f, 127 / 255.0f, 127 / 255.0f, 1);
            image.color = color;
        }
        else
        {
            color = new Vector4(255 / 255.0f, 156 / 256.0f, 0, 1);
            image.color = color;
        }
    }

    public TMP_Text CurrencyConversion(float money,TMP_Text text)
    {
        if (money > 1000)
        {
            text.text = (money / 1000).ToString("F") + "K";
        }
        else if (money > 10000)
            text.text = (money / 10000).ToString("f") + "B";
        else
            text.text = money.ToString();

        GarbageColorChangedButton?.Invoke(_valueHandler.Money, _addRewardGarbage.Money, _addRewardGarbage.ImageButton);
        WorkerColorChangedButton?.Invoke(_valueHandler.Money, _addWorker.Money,_addWorker.ImageButtonColor);
        SeedWorkerColorChangedButton?.Invoke(_valueHandler.Money, _speedWorkers.Money,_speedWorkers._ImageButtonColor);
        return text;
    }
}
