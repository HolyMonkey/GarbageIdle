using Agava.YandexGames;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderBoardDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerScore;
    [SerializeField] private TMP_Text[] _ranks;
    [SerializeField] private TMP_Text[] _leaderNames;
    [SerializeField] private TMP_Text[] _scoreList;
    [SerializeField] private string _leaderboardName = "LeaderBoard";

    private int _level;

    private void Awake()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        YandexGamesSdk.CallbackLogging = true;
#endif
        //Character character = FindObjectOfType<Character>();
        //_money = (int)character.Money;
        _level = PlayerPrefs.GetInt("SetLevelBar");
        StartCoroutine(Start());
    }

    

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        // Always wait for it if invoking something immediately in the first scene.
        yield return YandexGamesSdk.WaitForInitialization();

        PlayerAccount.RequestPersonalProfileDataPermission();
        if (!PlayerAccount.IsAuthorized)
            PlayerAccount.Authorize();
    }

    public void OpenLeaderboard()
    {
        //#if UNITY_WEBGL && !UNITY_EDITOR

        StartCoroutine(Start());

        Leaderboard.GetEntries(_leaderboardName, (result) =>
        {
            
            int leadersNumber = result.entries.Length >= _leaderNames.Length ? _leaderNames.Length : result.entries.Length;
            for (int i = 0; i < leadersNumber; i++)
            {
                string name = result.entries[i].player.publicName;
                Debug.Log("Èìÿ: " + name);
                if (string.IsNullOrEmpty(name))
                    name = "Anonimus";

                _leaderNames[i].text = name;
                _scoreList[i].text = result.entries[i].formattedScore;
                _ranks[i].text = result.entries[i].rank.ToString();
            }
        },
        (error) =>
        {
            //_logInPanel.Show();
        });
//#endif
    }

    public void SetLeaderboardScore()
    {
        //#if UNITY_WEBGL && !UNITY_EDITOR
        Leaderboard.GetPlayerEntry(_leaderboardName, OnSuccessCallback);
            
//#endif
    }

    private void OnSuccessCallback(LeaderboardEntryResponse result)
    {
        if (result==null || _level > result.score)
        {
            Leaderboard.SetScore(_leaderboardName, _level);
        }      
    }
}
