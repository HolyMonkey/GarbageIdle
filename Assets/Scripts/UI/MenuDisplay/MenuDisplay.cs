using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDisplay : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RewardedAd _rewardedAd;
    [SerializeField] private LeaderBoardButton _leaderBoardButton;
    [SerializeField] private RewardedAdView _rewardedAdView;
    [SerializeField] private Button _rewardedAdButton;

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();

    }


    private void Start()
    {
        
        Hide();
        //gameObject.SetActive(true);
    }

    public void Show()
    {

        _rewardedAdButton.gameObject.SetActive(true);
        _rewardedAdView.ShowReward();
        _leaderBoardButton.gameObject.SetActive(true);

    }
    public void Hide()
    {
        _rewardedAdView.ShowReward();
        _rewardedAdButton.gameObject.SetActive(false);
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
