using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeReward : MonoBehaviour
{
    [SerializeField] private ValueHandler _valueHandler;
    [SerializeField] private GarbageCan _garbageCan;
    [SerializeField] private Bar _bar;

    private float _reward = 500;

    private const string ScaleButton = "Scale";

    private void Start()
    {
        if (_garbageCan.CurrentQuantity >= _garbageCan.MaxQuantity)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);

        if (PlayerPrefs.HasKey("TakeReward"))
            _reward = PlayerPrefs.GetFloat("TakeReward");
    }

    public void Reward()
    {
        _valueHandler.AddReward(_reward);
        _garbageCan.ResetProgress();
        _bar.RaiseLevel();
        SetReward();
    }

    private void SetReward()
    {
        _reward += 500;
        PlayerPrefs.SetFloat("TakeReward", _reward);
        gameObject.SetActive(false);
    }
}
