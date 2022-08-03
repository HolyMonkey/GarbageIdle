using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] private GarbageCan _garbageCan;
    [SerializeField] private Image _slider;
    [SerializeField] private TMP_Text _textLvl;

    private int _level = 0;

    private void OnEnable()
    {
        _garbageCan.GarbageCountChanged += OnValueChangad;
        _slider.fillAmount = 0;
    }

    private void OnDisable()
    {
        _garbageCan.GarbageCountChanged -= OnValueChangad;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("SetLevelBar"))
            _level = PlayerPrefs.GetInt("SetLevelBar");

        _textLvl.text = _level.ToString();
    }

    public void OnValueChangad(int value,int maxValue)
    {
        _slider.fillAmount = (float)value / maxValue;
        //if (_slider.fillAmount >= 1)
        //    RaiseLevel();
    }

    public void RaiseLevel()
    {
        _level++;
        PlayerPrefs.SetInt("SetLevelBar", _level);
        _textLvl.text = _level.ToString();
    }
}
