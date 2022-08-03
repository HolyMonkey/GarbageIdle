using Agava.YandexGames.Utility;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundMuteHandler : MonoBehaviour
{
    [SerializeField] private Sprite _mute;
    [SerializeField] private Sprite _unmute;
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;

    private bool _isSoundMute;

    private void OnEnable()
    {
        _button.onClick.AddListener(SoundMuteButtonOn);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(SoundMuteButtonOn);
    }

    private void Start()
    {
        Debug.Log(_isSoundMute);
        if(PlayerPrefs.HasKey("ISSOUND"))
        {
            _isSoundMute = Convert.ToBoolean(PlayerPrefs.GetInt("ISSOUND"));
        }
        else
        {
            PlayerPrefs.SetInt("ISSOUND", Convert.ToInt32(true));
            _isSoundMute = false;
        }

        Debug.Log(_isSoundMute);
        if (_isSoundMute == true)
        {
            AudioListener.pause = true;
            AudioListener.volume = 0f;
            _image.sprite = _mute;
        }
        else
        {
            AudioListener.pause = false;
            AudioListener.volume = 0.8f;
            _image.sprite = _unmute;
        }
    }

    private void SoundMuteButtonOn()
    {
        Debug.Log(_isSoundMute);
        if (_isSoundMute == false)
        {
            AudioListener.pause = true;
            _isSoundMute = true;
            PlayerPrefs.SetInt("ISSOUND", Convert.ToInt32(_isSoundMute));
            _image.sprite = _mute;
            AudioListener.volume = 0f;
        }
        else
        {
            AudioListener.pause = false;
            _isSoundMute = false;
            _image.sprite = _unmute;
            PlayerPrefs.SetInt("ISSOUND", Convert.ToInt32(_isSoundMute));
            AudioListener.volume = 0.8f;
        }
        Debug.Log(_isSoundMute);
    }

    private void Update()
    {
        if (_isSoundMute == true)
            return;
        AudioListener.pause = WebApplication.InBackground;

    }

    public void OnApplicationFocus(bool hasFocus)
    {
        AudioListener.pause = !hasFocus;
    }
}

