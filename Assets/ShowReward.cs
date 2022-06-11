using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowReward : MonoBehaviour
{
    [SerializeField] private TakeReward _takeReward;
    [SerializeField] private TMP_Text _moneyInfoReward;

    public float _elepsedTime = 0;

    void Start()
    {
        gameObject.SetActive(false);
        _moneyInfoReward.alpha = 0;
    }

    void Update()
    {
        _elepsedTime += Time.deltaTime;

        if (_elepsedTime < 2f)
            Visible();
        
        if (_elepsedTime >= 2f)
            Invisible();
    }

    private void Visible()
    {
        _moneyInfoReward.alpha = Mathf.MoveTowards(_moneyInfoReward.alpha, 1, 1 * Time.deltaTime);
        _moneyInfoReward.text = "+" + _takeReward.RewardInfo.ToString();
    }

    private void Invisible()
    {
        _moneyInfoReward.alpha = Mathf.MoveTowards(_moneyInfoReward.alpha, 1, -_moneyInfoReward.alpha * Time.deltaTime);
        if (_moneyInfoReward.alpha <= 0.5f)
        {
            _elepsedTime = 0;
            gameObject.SetActive(false);
        }
    }
}
