using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDisplay : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RewardedAd _rewardedAd;
    [SerializeField] private LeaderBoardButton _leaderBoardButton;
    [SerializeField] private RewardedAdView _rewardedAdView;

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
    }


    private void Start()
    {
        
        Hide();
    }

    public void Show()
    {
        _rewardedAd.gameObject.SetActive(true);
        _leaderBoardButton.gameObject.SetActive(true);
        _rewardedAdView.ShowReward();
    }
    public void Hide()
    {
        _rewardedAd.gameObject.SetActive(false);
        _leaderBoardButton.gameObject.SetActive(false);
    }




    //public void Show()
    //{
    //    _canvasGroup.alpha = 1f;
    //}
    //public void Hide()
    //{
    //    _canvasGroup.alpha = 0;
    //}
}
