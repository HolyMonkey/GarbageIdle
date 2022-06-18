using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Data))]
public class GarbageCan : MonoBehaviour
{
    [SerializeField] private TakeReward _takeReward;
    [SerializeField] private FinishScene _finishScene;
    [SerializeField] private PointStart _pointStart;
    [SerializeField] private IncreaseMovementSpeed _increaseMovementSpeed;

    private Data _data;
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
        _data = GetComponent<Data>();

        _currentQuantity = _data.GetSave(NameCurrentQantity, _currentQuantity);
        _maxQuantity = _data.GetSave(NameMaxQantity, _maxQuantity);
        _totalAmountGarbage = _data.GetSave(NameTotalAmountGarbage, _totalAmountGarbage);
        _maxCount = _data.GetSave(MaxCount, _maxCount);
        GarbageCountChanged?.Invoke(_currentQuantity, _maxQuantity);
    }

    private void Update()
    {

        if (_currentQuantity >= _maxQuantity)
        {
            _takeReward.gameObject.SetActive(true);
        }
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

        if (_totalAmountGarbage >= 100)
        {
            _finishScene.OpenScrinFinish();
            _increaseMovementSpeed.DisableButton();
            ResetMaxCount();
        }
    }

    public void ResetMaxCount()
    {
        _totalAmountGarbage = 0;
        _maxCount = 100;
        PlayerPrefs.SetInt("MaxCount", _maxCount);
        PlayerPrefs.SetInt("TotalAmountGarbage", _totalAmountGarbage);
        _pointStart.Reset(_maxCount);
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
}
