using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Agava.YandexGames;

public class LeaderBoardPosition : MonoBehaviour
{
    [SerializeField] private TMP_Text _numberPosition;
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private TMP_Text _score;

    public void Init(LeaderboardEntryResponse leaderboardEntryResponse)
    {
        _numberPosition.text = leaderboardEntryResponse.rank.ToString();

        string name = leaderboardEntryResponse.player.publicName;
        if (string.IsNullOrEmpty(name))
            name = "Anonymous";
        Debug.Log($"My id = {leaderboardEntryResponse.player.uniqueID}, name = {name}");
        _playerName.text = name;
        _score.text = leaderboardEntryResponse.formattedScore;
    }
}
