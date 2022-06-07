using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarbageCan : MonoBehaviour
{
    [SerializeField] private TakeReward _takeReward;
    [SerializeField] private FinishScene _finishScene;

    private int _currentQuantity =0;
    private int _maxQuantity = 10;
    private int _totalAmountGarbage = 0;
    private int _maxCount = 100;

    public int CurrentQuantity => _currentQuantity;
    public int MaxQuantity => _maxQuantity;

    public event Action<int, int> GarbageCountChanged;

    public const string NameCurrentQantity = "CurrentQantityGarbage";
    public const string NameMaxQantity = "MaxQantity";
    public const string NameTotalAmountGarbage = "TotalAmountGarbage";
    public const string MaxCount = "MaxCount";

    private void Start()
    {
        _currentQuantity = GetSave(NameCurrentQantity, _currentQuantity);
        _maxQuantity = GetSave(NameMaxQantity, _maxQuantity);
        _totalAmountGarbage = GetSave(NameTotalAmountGarbage, _totalAmountGarbage);
        _maxCount = GetSave(MaxCount, _maxCount);
        GarbageCountChanged?.Invoke(_currentQuantity, _maxQuantity);
    }

    public void AddCount()
    {
        _currentQuantity++;
        _totalAmountGarbage++;
        _maxCount--;
        if (_maxCount < 0)
            _maxCount = 0;
        
        PlayerPrefs.SetInt("TotalAmountGarbage", _totalAmountGarbage);
        PlayerPrefs.SetInt("CurrentQantityGarbage", _currentQuantity);
        PlayerPrefs.SetInt("MaxQantity", _maxQuantity);
        PlayerPrefs.SetInt("MaxCount", _maxCount);
        GarbageCountChanged?.Invoke(_currentQuantity, _maxQuantity);

        if (_maxCount == 0)
        {
            _finishScene.OpenScrinFinish();
            ResetMaxCount();
        }

        if (_currentQuantity >= _maxQuantity)
            _takeReward.gameObject.SetActive(true);
        else
            return;

    }

    public void ResetMaxCount()
    {
        _maxCount = 100;
        PlayerPrefs.SetInt("MaxCount", _maxCount);
    }

    public void ResetProgress()
    {
        if (_currentQuantity > _maxQuantity)
        {
            int count = _currentQuantity - _maxQuantity;
            _maxQuantity += 10;
            GarbageCountChanged?.Invoke(0, 1);
            _currentQuantity = count;
            GarbageCountChanged?.Invoke(_currentQuantity, _maxQuantity);
        }
        else if(_currentQuantity == _maxQuantity)
        {
            _currentQuantity = 0;
            _maxQuantity += 10;
            GarbageCountChanged?.Invoke(0, 1);
        }
    }

    private int GetSave(string name,int value)
    {
        if (PlayerPrefs.HasKey(name))
        {
            value = PlayerPrefs.GetInt(name);
        }
        return value;
    }
}
