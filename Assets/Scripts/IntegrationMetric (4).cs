using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameAnalyticsSDK;
using UnityEngine;

public class IntegrationMetric
{
    private const string SessionCountName = "sessionCount";
    private const string CurrentSoftName = "CurrentSoft";
    private const string _regDay = "regDay";
    private const string ProfileId = "ProfileId";
    private const int ProfileIdLength = 10;

    public int SessionCount;
    public int CurrentSoft; 
    private string _profileId;

    public void OnGameStart()
    {
        Dictionary<string, object> count = new Dictionary<string, object>();
        count.Add("count", CountSession());
        AppMetrica.Instance.ReportEvent("game_start", count);
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "game_start", count);
    }

    public void OnLevelStart(int levelIndex)
    {
        var levelProperty = CreateLevelProperty(levelIndex);
        AppMetrica.Instance.ReportEvent("level_start", levelProperty);
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "level_start", levelProperty);
    }

    public void OnLevelComplete(int levelComplitioTime, int levelIndex)
    {
        Dictionary<string, object> userInfo = new Dictionary<string, object> { { "level", levelIndex }, { "time_spent", levelComplitioTime } };

        AppMetrica.Instance.ReportEvent("level_complete", userInfo);
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "level_complete", userInfo);
    }

    public void OnLevelFail(int levelFailTime, int levelIndex)
    {
        Dictionary<string, object> userInfo = new Dictionary<string, object> { { "level", levelIndex }, { "time_spent", levelFailTime } };

        AppMetrica.Instance.ReportEvent("fail", userInfo);
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "fail", userInfo);
    }

    public void OnRestartLevel(int levelIndex)
    {
        var levelProperty = CreateLevelProperty(levelIndex);
        AppMetrica.Instance.ReportEvent("restart", levelProperty);
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "restart", levelProperty);
    }

    public void OnSoftCurrencySpend(string type, string name, int currencySpend,int numberUses,int money)
    {
        Dictionary<string, object> userInfo = new Dictionary<string, object> { { "type", type }, { "name", name }, { "amount", currencySpend }, { "count", numberUses } };
        YandexAppMetricaUserProfile userProfile = new YandexAppMetricaUserProfile();
        userProfile.Apply(YandexAppMetricaAttribute.CustomCounter("current_soft").WithDelta(money));
        ReportUserProfile(userProfile);
        //Amplitude.Instance.logEvent("soft_spent", userInfo);
        AppMetrica.Instance.ReportEvent("soft_spent", userInfo);
    }

    public void OnCurrentSoftBalance(int money)
    {
        Dictionary<string, object> balanceMone = new Dictionary<string, object>();
        balanceMone.Add("balance", CurrentBalance(money));
    }

    public void SetUserProperty()
    {
        //amplitude.setUserProperty("session_count", SessionCount);

        YandexAppMetricaUserProfile userProfile = new YandexAppMetricaUserProfile();
        userProfile.Apply(YandexAppMetricaAttribute.CustomCounter("session_count").WithDelta(SessionCount));
        ReportUserProfile(userProfile);

        if (PlayerPrefs.HasKey(_regDay) == false)
        {
            RegDay();
        }
        else
        {
            int firstDay = PlayerPrefs.GetInt(_regDay);
            int daysInGame = DateTime.Now.Day - firstDay;

            DaysInGame( daysInGame);
        }
    }

    private void RegDay()
    {
        //amplitude.setUserProperty("reg_day", DateTime.Now.ToString());

        YandexAppMetricaUserProfile userProfile = new YandexAppMetricaUserProfile();
        userProfile.Apply(YandexAppMetricaAttribute.CustomString("reg_day").WithValue(DateTime.Now.ToString()));
        ReportUserProfile(userProfile);

        PlayerPrefs.SetInt(_regDay, DateTime.Now.Day);
    }

    private void DaysInGame( int daysInGame)
    {
        //amplitude.setUserProperty("days_in_game", daysInGame);

        YandexAppMetricaUserProfile userProfile = new YandexAppMetricaUserProfile();
        userProfile.Apply(YandexAppMetricaAttribute.CustomCounter("days_in_game").WithDelta(daysInGame));
        ReportUserProfile(userProfile);
    }

    private void ReportUserProfile(YandexAppMetricaUserProfile userProfile)
    {
        AppMetrica.Instance.SetUserProfileID(GetProfileId());
        AppMetrica.Instance.ReportUserProfile(userProfile);
    }

    private string GetProfileId()
    {
        if (PlayerPrefs.HasKey(ProfileId))
        {
            _profileId = PlayerPrefs.GetString(ProfileId);
        }
        else
        {
            _profileId = GenerateProfileId(ProfileIdLength);
            PlayerPrefs.SetString(ProfileId, _profileId);
        }

        return _profileId;
    }

    private string GenerateProfileId(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";

        var random = new System.Random();

        return new string(Enumerable.Repeat(chars, length)
            .Select(letter => letter[random.Next(letter.Length)]).ToArray());
    }

    private int CurrentBalance(int money)
    {
        int balanceMoney = money;

        if (PlayerPrefs.HasKey(CurrentSoftName))
        {
            balanceMoney = PlayerPrefs.GetInt(CurrentSoftName);
            CurrentSoft = balanceMoney; 
        }

        PlayerPrefs.SetInt(CurrentSoftName, balanceMoney);
        CurrentSoft = balanceMoney;

        return balanceMoney;
    }

    private Dictionary<string, object> CreateLevelProperty( int levelIndex)
    {
        Dictionary<string, object> level = new Dictionary<string, object>();
        level.Add("level", levelIndex);

        return level;
    }

    private int CountSession()
    {
        int count = 1;

        if (PlayerPrefs.HasKey(SessionCountName))
        {
            count = PlayerPrefs.GetInt(SessionCountName);
            count++;
        }

        PlayerPrefs.SetInt(SessionCountName, count);
        SessionCount = count;

        return count;
    }
}
