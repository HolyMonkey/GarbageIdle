using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LeaderBoardButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private LeaderBoardDisplay _leaderBoardDisplay;
    [SerializeField] private Button _closeButton;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
        _closeButton.onClick.AddListener(OnCloseClock);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
        _closeButton.onClick.RemoveListener(OnCloseClock);
    }

    private void OnCloseClock()
    {
        Hide();
    }

    private void OnClick()
    {
        if (_leaderBoardDisplay.gameObject.activeSelf)
            Hide();
        else
            Show();
    }

    private void Show()
    {
        _leaderBoardDisplay.gameObject.SetActive(true);
        _leaderBoardDisplay.SetLeaderboardScore();
        _leaderBoardDisplay.OpenLeaderboard();
    }

    private void Hide()
    {
        _leaderBoardDisplay.gameObject.SetActive(false);
    }
}
