using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TakeReward : MonoBehaviour
{
    [SerializeField] private ValueHandler _valueHandler;
    [SerializeField] private GarbageCan _garbageCan;
    [SerializeField] private Bar _bar;
    [SerializeField] private TMP_Text _moneyInfoReward;
    [SerializeField] private ShowReward _showReward;

    private float _reward = 500;
    private float _rewardInfo;

    public float RewardInfo => _rewardInfo;

    private const string TakeRewardMoney = "TakeReward";

    private void OnValidate()
    {
        _showReward = FindObjectOfType<ShowReward>();
    }

    private void Start()
    {
        if (_garbageCan.CurrentQuantity >= _garbageCan.MaxQuantity)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);

        if (PlayerPrefs.HasKey(TakeRewardMoney))
            _reward = PlayerPrefs.GetFloat(TakeRewardMoney);

    }

    public void Reward()
    {
        _rewardInfo = _reward;
        _showReward.Visible();
        _valueHandler.AddReward(_reward);
        _garbageCan.ResetProgress();
        _bar.RaiseLevel();
        SetReward();
    }

    private void SetReward()
    {
        _reward += 500;
        PlayerPrefs.SetFloat(TakeRewardMoney, _reward);
        gameObject.SetActive(false);
    }
}
