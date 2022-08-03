using Agava.YandexGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardedAd : MonoBehaviour
{
    [SerializeField] private Button _button;

    private ValueHandler _valueHandler;
    //private Character _character;

    private void Start()
    {
        InterstitialAd.Show();
        _valueHandler = FindObjectOfType<ValueHandler>();
    }
    private Action _adOpened;
    private Action _adRewarded;
    private Action _adClosed;
    private Action<string> _adErrorOccured;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
        _adOpened += OnAdOpened;
        _adRewarded += OnAdRewarded;
        _adClosed += OnAdClosed;
        _adErrorOccured += OnAdErrorOccured;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
        _adOpened -= OnAdOpened;
        _adRewarded -= OnAdRewarded;
        _adClosed -= OnAdClosed;
        _adErrorOccured -= OnAdErrorOccured;
    }

    public void OnClick()
    {


#if UNITY_WEBGL && !UNITY_EDITOR
        VideoAd.Show(_adOpened, _adRewarded, _adClosed, _adErrorOccured);
        
#endif
#if UNITY_EDITOR
        _valueHandler = FindObjectOfType<ValueHandler>();
        _valueHandler.AddReward(_valueHandler.Money/3);
#endif

    }

    private void OnAdErrorOccured(string obj)
    {
        Debug.Log(obj);
        AudioListener.pause = false;
        AudioListener.volume = 0f;
    }

    private void OnAdClosed()
    {


        //if (!Convert.ToBoolean(PlayerPrefs.GetInt(KeySave.SoundOn)))
        //{
        //    AudioListener.pause = false;
        //    AudioListener.volume = 0.8f;
            
        //}
    }

    private void OnAdRewarded()
    {
        _valueHandler = FindObjectOfType<ValueHandler>();
        _valueHandler.AddReward(_valueHandler.Money / 3);
        //_character.ApplyMoney(50);
    }

    private void OnAdOpened()
    {
        AudioListener.pause = true;
        AudioListener.volume = 0f;
    }
}
